@echo off
rem Copyright IBM Corp. 2009  All rights reserved.                 
rem                                                                    
rem This sample program is owned by International Business Machines    
rem Corporation or one of its subsidiaries ("IBM") and is copyrighted  
rem and licensed, not sold.                                            
rem                                                                    
rem You may copy, modify, and distribute this sample program in any    
rem form without payment to IBM,  for any purpose including developing,
rem using, marketing or distributing programs that include or are      
rem derivative works of the sample program. Licenses to IBM patents    
rem are expressly excluded from this license.                         
rem The sample program is provided to you on an "AS IS" basis, without 
rem warranty of any kind.  IBM HEREBY  EXPRESSLY DISCLAIMS ALL         
rem WARRANTIES EITHER EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO
rem THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTIC-
rem ULAR PURPOSE. Some jurisdictions do not allow for the exclusion or 
rem limitation of implied warranties, so the above limitations or      
rem exclusions may not apply to you.  IBM shall not be liable for any  
rem damages you suffer as a result of using, modifying or distributing 
rem the sample program or its derivatives.                             
rem                                                                    
rem Each copy of any portion of this sample program or any derivative  
rem work,  must include the above copyright notice and disclaimer of   
rem warranty.                                                          
                                                               

rem BATCH FILE: buildApp.bat 
rem Builds C# routines 
rem Usage: buildApp.bat CSFilename dotnetversion
rem Eg: buildApp.bat Program.cs netf20
rem To compile and run the samples, you must have 
rem Version 2.0 or later of the .NET Framework installed.
   
set VERSION=%2

rem Compile the program.
rem The below command is used to compile/build a .cs file
rem In this the "C:\IBM DATA SERVER DRIVER\bin" refers to the path where 'Data Server Driver' package is installed. 
rem Please change "C:\IBM DATA SERVER DRIVER\bin" in the below command to the path 
rem where 'Data Server Driver' package is installed on your m/c before running the batch file

csc /debug:full /nologo /r:System.dll /r:System.XML.dll /lib:"C:\Program Files\ibm\IBM DATA SERVER DRIVER\bin"\%VERSION%\ /r:System.Data.dll  /r:mscorlib.dll /target:exe /reference:"C:\Program Files\Informix Client-SDK\bin\netf20\IBM.Data.Informix.dll" "%1"

:exit
