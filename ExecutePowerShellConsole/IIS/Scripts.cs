using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutePowerShellConsole.IIS
{
  public class Scripts
  {
    /// <summary>
    /// 
    /// </summary>
    public static string CheckForIISEnable { get; private set; } = @"
        <#
           This script queries the Windows Managment Instrumentaiton (WMI) to
           check if the World Wide Wide Publishing Service (W3SVC) is installed, which is part of the IIS.
        #>
        $installed = Get-WmiObject -Query 'SELECT * FROM Win32_Service Where Name=''W3SVC'''
        if ($installed)
        {
            echo 'true'
        } else {
            echo 'false'
        }
    ";

    /// <summary>
    /// 
    /// </summary>
    public static string CreateSelfSignedCert { get; private set; } = $@"
      Import-Module WebAdministration;
      $hostname = [System.Net.Dns]::GetHostName();
      New-SelfSignedCertificate -DnsName $hostname -CertStoreLocation 'Cert:\LocalMachine\My' -FriendlyName 'HealthCheckApp_Cert';
    ";

    public static string AddNewWebsiteToIIS { get; private set; } = @"
      Import-Module WebAdministration;

      $hostname = [System.Net.Dns]::GetHostName();
      New-SelfSignedCertificate -DnsName $hostname -CertStoreLocation 'Cert:\LocalMachine\My' -FriendlyName 'HealthCheckApp_Cert';

      # Variables
      $siteName = 'HealthCheckAPI'; 
      $appPoolName = 'HealthCheckAppPool';
      $physicalPath = 'C:\inetpub\wwwroot\HealthCheckAPI'; 
      $portHttp = 40080
      $portHttps = 40443
      $bindingInfoHttp = 'http/*:${portHttp}:localhost';
      $bindingInfoHttps = 'https/*:${portHttps}:localhost'; 

      # Create the physicalPath folder if it doesn't exist
      if (!(Test-Path -Path $physicalPath)) {
          New-Item -Path $physicalPath -ItemType Directory
      }

      # Check if the site already exists
      if (Test-Path 'IIS:\Sites\$siteName') {
          Write-Host 'Website $siteName already exists.'
      } else {
          # Create a new application pool
          if (!(Test-Path 'IIS:\AppPools\$appPoolName')) {
              New-WebAppPool -Name $appPoolName;
              Write-Host 'Application Pool $appPoolName created successfully.'
          } else {
              Write-Host 'Application Pool $appPoolName already exists.'
          }

          # Create a new IIS website with HTTP binding
          New-Website -Name $siteName -PhysicalPath $physicalPath -ApplicationPool $appPoolName -Port $portHttp -HostHeader 'localhost'
          Write-Host 'Website $siteName created successfully.'

          # Configure the site binding for https
          $cert = Get-ChildItem -Path cert:\LocalMachine\My | Where-Object { $_.FriendlyName -eq 'HealthCheckApp_Cert' }
          if ($cert) {
              New-WebBinding -Name $siteName -Protocol 'https' -Port $portHttps -HostHeader 'localhost'
              $bindingSiteHttps = '0.0.0.0!'+ $portHttps
              Push-Location IIS:\SslBindings
              New-Item $bindingSiteHttps -Value $cert
              Pop-Location
              Write-Host 'HTTPS binding configured successfully.'
          } else {
              Write-Host 'Certificate 'HealthCheckApp_Cert' not found.'
          }

          Write-Host 'HTTP and HTTPS bindings configured successfully.'
      }
    ";

  }
}
