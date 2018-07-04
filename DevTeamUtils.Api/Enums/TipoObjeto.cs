using System.ComponentModel;

namespace DevTeamUtils.Api.Enums
{
    public enum TipoObjeto
    {
        [Description("TB = Tabela de Cadastro")]
        TabelaCadastro,
        [Description("TB_ITEM = Tabela de Detalhe")]
        TabelaDetalhe,
        [Description("TM = Tabela de Movimentação")]
        TabelaMovimentacao,
        [Description("TE = Tabela de Estáticas/Domínio")]
        TabelaEstatistica,
        [Description("TL = Tabela de Log de Operação")]
        TabelaLog,
        [Description("TP = Tabela Temporária")]
        TabelaTemporaria,
        [Description("VW = View")]
        View,
        [Description("SQ = Sequence")]
        Sequence,
        [Description("FC = Function")]
        Function,
        [Description("SP = Stored Procedure")]
        StoredProcedure,
        [Description("TIA = Trigger Insert After")]
        TriggerInsertAfter,
        [Description("TIB = Trigger Insert Before")]
        TriggerInsertBefore,
        [Description("TUA = Trigger Update After")]
        TriggerUpdateAfter,
        [Description("TUB = Trigger Update Before")]
        TriggerUpdateBefore,
        [Description("TDA = Trigger Delete After")]
        TriggerDeleteAfter,
        [Description("TDB = Trigger Delete Before")]
        TriggerDeleteBefore,
        [Description("PKG = Package")]
        Package,
        [Description("JOB = Job")]
        Job
    }
}
