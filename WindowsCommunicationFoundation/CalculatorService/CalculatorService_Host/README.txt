
netsh http add urlacl url=http://+:8081/ user=desktop-644g20u\alvar
resutl --> URL reservation successfully added

netsh http add urlacl url=http://+:8080/CalculatorServiceSvc/ user=desktop-644g20u\alvar
resutl -->  URL reservation successfully added

netsh http show urlacl url=http://+:8080/CalculatorServiceSvc/

netsh http delete urlacl url=http://+:8080/CalculatorServiceSvc/

netsh http add urlacl url=http://+:8765/CalculatorServiceSvc/ user=desktop-644g20u\alvar


netsh http add urlacl url=http://+:8765/CalculatorServiceSvc.svc user=desktop-644g20u\alvar



command prompt 

netstat -ano | find :port_number 

tasklist | findstr PID_to_portnumber

taskkile /PID PID_Number /F



sc create CalculatorServiceHost binPath="C:\Workstation\CalculatorService\CalculatorService_Host\bin\Debug\CalculatorService_Host.exe"
sc start CalculatorServiceHost
sc stop CalculatorServiceHost
sc delete CalculatorServiceHost