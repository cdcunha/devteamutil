using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUtils.Models.EqualityComparer
{
    class ConexaoBDEqualityComparer : EqualityComparer<ConexaoBD>
    {
        private static readonly ConexaoBDEqualityComparer _instance = new ConexaoBDEqualityComparer();
        public static ConexaoBDEqualityComparer Instance { get { return _instance; } }

        private ConexaoBDEqualityComparer() { }

        public override bool Equals(ConexaoBD x, ConexaoBD y)
        {
            return x.NomeServidor.ToUpperInvariant() == y.NomeServidor.ToUpperInvariant() &&
                x.Tipo.ToUpperInvariant() == y.Tipo.ToUpperInvariant();
        }

        public override int GetHashCode(ConexaoBD obj)
        {
            return obj.NomeServidor.ToUpperInvariant().GetHashCode() ^ obj.Tipo.ToUpperInvariant().GetHashCode();
        }
    }
}
