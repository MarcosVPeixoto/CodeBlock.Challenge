using CodeBlock.Challenge.Domain.Entities;
using CodeBlock.Challenge.IBusiness.IBusiness;
using Newtonsoft.Json;

namespace CodeBlock.Challenge.Business.Business
{
    public class CargaSemanalBusiness : ICargaSemanalBusiness
    {
        public CargaSemanalDto GetCargaDisponivelSemanal(DateTime data)
        {
            var semana = (data.Day / 4) + 1;
            if (semana > 4) semana = 4;
            var requisicao = new HttpClient();
            var response = requisicao.GetAsync($"https://fuct-smk186-code-challenge.cblx.com.br/minerais?mes={data.Month}&ano={data.Year}&semana={semana}");
            var content = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CargaSemanalDto>(content);
        }
    }
}
