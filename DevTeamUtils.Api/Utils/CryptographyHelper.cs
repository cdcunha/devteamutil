using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Utils
{
    public static class CryptographyHelper
    {
        public static string EncryptPassword(string passowrd)
        {
            int[] tNumrChve = new int[10];
            string passowrdEncrypt = "";
            string sTemp;
            int chave;
            int lAscAnte = 0;
            int lAscAtua = 0;
            int i, nTamChave;

            tNumrChve[0] = 17;
            tNumrChve[1] = 30;
            tNumrChve[2] = 11;
            tNumrChve[3] = 27;
            tNumrChve[4] = 21;
            tNumrChve[5] = 15;
            tNumrChve[6] = 22;
            tNumrChve[7] = 18;
            tNumrChve[8] = 23;
            tNumrChve[9] = 19;

            chave = 654321 * 7;

            nTamChave = passowrd.Length;

            //O primeiro byte da string retornada é o tamanho real da string a ser criptografada
            passowrdEncrypt = ((char)(nTamChave + 29)).ToString();

            //Complementa o string com brancos
            passowrd = passowrd.PadRight(15, ' ');

            //Alteracao para a funcao de encriptografar nao gerar os Caracteres " e '
            //Bruno 11/12/2002

            for (i = 0; i < passowrd.Length; i++)
            {
                int modKey;
                if (i >= 9)
                {
                    modKey = tNumrChve[i % 9];
                }
                else
                {
                    modKey = tNumrChve[i % 9 + 1];
                }
                int keyAces = (chave + lAscAnte);
                int cal = (int)passowrd[i] + (keyAces % modKey);

                if (cal == 34 || cal == 39)
                    lAscAtua = passowrd[i];
                else
                    lAscAtua = cal;

                passowrdEncrypt += (char)lAscAtua;
                lAscAnte = lAscAtua;
            }

            sTemp = "";
            //Verifica se deseja string para arquivos .INI
            for (i = 0; i < passowrdEncrypt.Length; i++)
                sTemp += (255 - (int)passowrdEncrypt[i]).ToString("X");
            passowrdEncrypt = sTemp;

            return passowrdEncrypt;
        }

        public static string DecryptPassword(string password)
        {
            string passwordDecrypt = "";
            string sTemp = "";
            int chave;
            int[] tNrKey = new int[10];
            int lAscBefore = 0;
            int lAscActual = 0;
            int i;
            char cChar;
            int nKeyLength;

            tNrKey[0] = 17;
            tNrKey[1] = 30;
            tNrKey[2] = 11;
            tNrKey[3] = 27;
            tNrKey[4] = 21;
            tNrKey[5] = 15;
            tNrKey[6] = 22;
            tNrKey[7] = 18;
            tNrKey[8] = 23;
            tNrKey[9] = 19;

            i = 0;
            //Converte hexa para ascii
            for (i = 0; i < password.Length; i += 2)
            {
                string hs = password.Substring(i, 2);
                sTemp += ((char)(255 - Convert.ToInt32(hs, 16)));
            }

            password = sTemp;

            //Obtem o tamanho da chave criptografada
            if (password.Length < 1)
                nKeyLength = 15;
            else
                nKeyLength = ((int)password[0]) - 29;

            //Pega apenas a chave, desprezando o restante
            password = password.Substring(1, nKeyLength);

            chave = 654321 * 7;

            for (i = 0; i < password.Length; i++)
            {
                int modKey;
                if (i >= 9)
                {
                    modKey = tNrKey[i % 9];
                }
                else
                {
                    modKey = tNrKey[i % 9 + 1];
                }
                int keyAces = (chave + lAscBefore);
                int cal = (int)password[i] + (keyAces % modKey);

                if (cal == 34 || cal == 39)
                    cChar = password[i];
                else
                {
                    lAscActual = (int)password[i];
                    cChar = (char)(lAscActual - (keyAces % modKey));
                }
                passwordDecrypt += cChar;
                lAscBefore = lAscActual;
            }
            return passwordDecrypt;
        }
    }
}
