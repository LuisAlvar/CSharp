param(
	[Parameter(Mandatory=$false)]
	[ValidateSet("enable","disable")]
	[string]$action,
	[Parameter(Mandatory=$false)]
	[switch]$status,
	[Parameter(Mandatory=$false)]
	[switch]$help
)

$stopMSMQServiceScript=".\ManageMSMQServices"

function Show_Help{
	<#
	.SYNOPSIS
		Script to turn MSMQ-Server Windows feature on or off based on the provided action. 
	
	.DESCRIPTION
		This script checks the status of the Microsoft Message Queuing (MSMQ) windows feature,
		and either enables or disables this window feature based on the action provided as an argument. 
	
	.PARAMETER action 
		The action to be performed on the window feature. Acceptable values are "enable" and "disable".
		
	.PARAMETER help
		Displays the help message for the script.
	
	.EXAMPLE 
		.\MSMessageQueueActivation.ps1 -action enable 
		Enables the MSQM-Server Windows feature, if disable.
	
	.EXAMPLE
		.\MSMessageQueueActivation.ps1 -action disable
		Disables the MSQM-Server Windows feature, if enable.
	
	.EXAMPLE 
		.\MSMessageQueueActivation.ps1 -status 
		Show current status of this windows features

	.EXAMPLE 
		.\MSMessageQueueActivation.ps1 -help
		Displays the help message
	
	.NOTES
		Requires administrative privileges to run - to prevent accidental execution. 
	
	#>
		Write-Output @"
Usage: .\MSMessageQueueActivation.ps1 [-action <on|off>] [-status] [-help]

Options:
	-action <enable|disable>  - Starts or stops the MSMQ service based on the provided action. 
	-status                   - Display the current status of Microsoft Message Queue with a machine.
	-help                     - Displays this help message.
"@
}

# Function to check if script is running with administrator privileges
function Test-Admin {
	$currentUser = [Security.Principal.WindowsIdentity]::GetCurrent();
	$administratorRole = [Security.Principal.WindowsBuiltInRole]::Administrator;
	return (New-Object Security.Principal.WindowsPrincipal($currentUser)).IsInRole($administratorRole)
}

function Manage_WindowFeature_MSMQ
{
	param([string]$action)

	switch ($action) {
		"enable" {
			Write-Output "Enabling MSMQ feature ..."
			Enable-WindowsOptionalFeature -FeatureName MSMQ-Server -All -Online
			Write-Output "MSMQ feature enabled."
		}
		"disable" {
			if (Test-Path $stopMSMQServiceScript) {
				Write-Output "Stoping MSMQ services"
				& $stopMSMQServiceScript -action off
			}
			else 
			{
				Write-Output "ManageMSMQServices.ps1 is not within the same directory as this script."
				exit
			}
			Write-Output "Disabling MSMQ feature ..."
			$result = Disable-WindowsOptionalFeature -FeatureName MSMQ-Server -Online -NoRestart
			Write-Output "MSMQ feature disabled."
			if ($result.RestartNeeded) {
				Write-Output "A reboot is required to complete the operation. Please restart your machine"
			}
		}
	}
}

function Current_Status_On_MSMQ_Feature
{
	param([string]$status)
	if ($status) {
		# Check if MSMQ if enable
		$msmqInstalled = Get-WindowsOptionalFeature -FeatureName MSMQ-Server -Online
		if ($msmqInstalled.State -ne 'Enabled') 
		{
			Write-Output "Windows feature MSMQ-Server is not installed. Need to enable MSMQ-Server".
		}
		else {
			Write-Output "Windows feature MSMQ-Server is already turn on"
			# Check MSMQ service status 
			$msmqService = Get-Service -Name MSMQ;
			if ($msmqService.Status -ne 'Running') 
			{
				Write-Output "MSMQ is not active. Please start MSMQ service (i.e., $($msmqService.DisplayName)) ....";
			}
			else {
				Write-Output "MSMQ [$($msmqService.DisplayName)] is already active (i.e., Running).";
			}
		}
	}
}

if (-not (Test-Admin))
{
	Write-Output "This script requires elevation (run as administrator). Please run this script as an Administrator."
	exit
}

if ($help) {
	Show_Help
	exit
}

if (-not $action -and $status) {
	Current_Status_On_MSMQ_Feature -status $status
	exit
}

if ($action) {
	Current_Status_On_MSMQ_Feature -status $status
	Manage_WindowFeature_MSMQ -action $action
	Current_Status_On_MSMQ_Feature -status $status
}

