using System.ComponentModel;

namespace DevTeamUtils.Api.Enums
{
    public enum EnumAtributoCampo
    {
        [Description("PK = Primary Key")]
        PrimaryKey = 1 ,
        [Description("FK = Foreign Key")]
        ForeignKey,
        [Description("CO = Código")]
        Codigo,
        [Description("NU = Número")]
        Numero,
        [Description("DH = Data/Hora")]
        DataHora,
        [Description("DS = Descrição")]
        Descricao,
        [Description("NO = Nome")]
        Nome,
        [Description("VL = Valor")]
        Valor,
        [Description("TP = Tipo")]
        Tipo,
        [Description("SN = Sim/Não")]
        SimNao,
        [Description("SG = Sigla")]
        Sigla,
        [Description("IM = Imagem/Arquivo")]
        ImagemArquivo,
        [Description("TX = Texto")]
        Texto,
        [Description("QT = Quantidade")]
        Quantidade,
        [Description("ST = Situação/Status")]
        SituacaoStatus,
        [Description("IN = Indicação")]
        Indicacao
    }
}
