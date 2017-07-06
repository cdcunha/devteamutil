using System.Collections.Generic;

namespace DevTeamUtils.Models.EqualityComparer
{
    class AgendaTelefonicaEqualityComparer : EqualityComparer<AgendaTelefonica>
    {
        private static readonly AgendaTelefonicaEqualityComparer _instance = new AgendaTelefonicaEqualityComparer();
        public static AgendaTelefonicaEqualityComparer Instance { get { return _instance; } }

        private AgendaTelefonicaEqualityComparer(){ }

        public override bool Equals(AgendaTelefonica x, AgendaTelefonica y)
        {
            return x.Nome.ToUpperInvariant() == y.Nome.ToUpperInvariant() && 
                x.Cargo.ToUpperInvariant() == y.Cargo.ToUpperInvariant();
        }

        public override int GetHashCode(AgendaTelefonica obj)
        {
            return obj.Nome.ToUpperInvariant().GetHashCode() ^ obj.Cargo.ToUpperInvariant().GetHashCode();
        }
    }
}
