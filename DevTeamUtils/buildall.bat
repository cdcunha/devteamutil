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
                                                               

rem BATCH FILE: buildAll.bat 
rem builds all files using .NET framework 2.0

rem Compile the programs.
rem IfxTestConnection.cs
CALL BuildApp.bat IfxTestConnection.cs netf40

:exit
