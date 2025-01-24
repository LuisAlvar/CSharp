param(
	[Parameter(Mandatory=$false)]
	[ValidateSet("on","off")]
	[string]$action,
	[Parameter(Mandatory=$false)]
	[switch]$help
)

function Show_Help{
<#
.SYNOPSIS
	Script to turn MSMQ service on or off based on the provided action. 

.DESCRIPTION
	This script checks the status of the Microsoft Message Queuing (MSMQ) service
	and either starts or stops the service based on the action provided as an argument. 

.PARAMETER action 
	The action to be performed on the MSMQ service. Acceptable values are "on" and "off".
	
.PARAMETER help
	Displays the help message for the script.

.EXAMPLE 
	.\Manage-MSMQ.ps1 -action on 
	Start the MSQM service if it is not already running.

.EXAMPLE
	.\Manage-MSMQ.ps1 -action off
	Stops the MSMQ service if it is running

.EXAMPLE 
	.\Manage_MSMQ.ps1 --help
	Displays the help message

.NOTES
	Requires administrative privileges to run - to prevent accidental execution. 

#>
	Write-Output @"
Usage: .\ManageMSMQ.ps1 [-action <on|off>] [-help]

Options:
	-action <on|off>  - Starts or stops the MSMQ service based on the provided action. 
	--help	          - Displays this help message.
"@
}

# Function to check if script is running with administrator privileges
function Test-Admin {
	$currentUser = [Security.Principal.WindowsIdentity]::GetCurrent();
	$administratorRole = [Security.Principal.WindowsBuiltInRole]::Administrator;
	return (New-Object Security.Principal.WindowsPrincipal($currentUser)).IsInRole($administratorRole)
}

function Manage_MSMQ {
	param([string]$action)

	$msmqService = Get-Service -Name MSMQ -ErrorAction SilentlyContinue

	if (-not $msmqService) {
		Write-Output "MSMQ service not found. Please ensure MSMQ is installed."
		return
	}

	switch($action) {
		"on" {
			if ($msmqService.Status -ne 'Running') {
				Write-Output "Starting MSMQ service ..."
				Start-Service -Name MSMQ
				Write-Output "MSMQ service started."
			} else {
				Write-Output "MSMQ service is already running."
			}
		}
		"off" {
			if ($msmqService.Status -eq 'Running') {
				Write-Output "Stopping MSMQ service ..."
				Stop-Service -Name MSMQ
				Write-Output "MSMQ service stopped."
			} else {
				Write-Output "MSMQ service is already stopped."
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

if (-not $action) {
	Write-Output "No action provided. Use -action <on|off> or --help for usage information."
	exit
}

Manage_MSMQ -action $action 