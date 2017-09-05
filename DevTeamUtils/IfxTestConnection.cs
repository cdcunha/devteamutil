/* Copyright IBM Corp. 2009  All rights reserved.                 */
/*                                                                   */
/*This sample program is owned by International Business Machines    */
/*Corporation or one of its subsidiaries ("IBM") and is copyrighted  */
/*and licensed, not sold.                                            */
/*                                                                   */
/*You may copy, modify, and distribute this sample program in any    */
/*form without payment to IBM,  for any purpose including developing,*/
/*using, marketing or distributing programs that include or are      */
/*derivative works of the sample program. Licenses to IBM patents    */
/*are expressly excluded from this license.                          */
/*The sample program is provided to you on an "AS IS" basis, without */
/*warranty of any kind.  IBM HEREBY  EXPRESSLY DISCLAIMS ALL         */
/*WARRANTIES EITHER EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO*/
/*THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTIC-*/
/*ULAR PURPOSE. Some jurisdictions do not allow for the exclusion or */
/*limitation of implied warranties, so the above limitations or      */
/*exclusions may not apply to you.  IBM shall not be liable for any  */
/*damages you suffer as a result of using, modifying or distributing */
/*the sample program or its derivatives.                             */
/*                                                                   */
/*Each copy of any portion of this sample program or any derivative  */
/*work,  must include the above copyright notice and disclaimer of   */
/*warranty.                                                          */
/*                                                                   */

using System;
using IBM.Data.Informix;

namespace IfxTestConnection
{
    public static class IfxTestConnection
    {
        /// <summary>
        /// The demo app needs to be invoked as  EXE -conn "connection string" -log "log filename"
        /// Eg: Transactions.exe //This will pick the connection string from ConnInfo.xml
        /// OR
        /// Transactions.exe -conn "Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;"
        /// OR
        /// Transactions.exe -conn "Database=perf;Server=localhost:9092;User ID=informix;Password=informix123;" -log log.txt
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                try
                {
                    IfxConnection conn = new IfxConnection(args[1]);
                    try
                    {
                        conn.Open();
                        string server = conn.ServerType;
                        string version = conn.ServerVersion;
                        string type = conn.ServerType;
                        conn.Close();
                        Console.Write($"{server}\n{type} versão {version}\n\nConectado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException == null)
                            Console.Write($"{conn.ServerType}\nErro ao tentar conectar: \n{ex.Message}");
                        else
                            Console.Write($"{conn.ServerType}\nErro ao tentar conectar: \n{ex.Message}\nDetalhe: {ex.InnerException.Message}");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException == null)
                        Console.Write(ex.Message);
                    else
                        Console.Write(ex.InnerException.Message);
                }
            }
            else
                Console.Write("É necessário passar a Connection String");
        }
    }
}
