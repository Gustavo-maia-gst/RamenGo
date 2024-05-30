using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;

namespace RamenGo.Application.Services
{
    public class ProteinService
    {
        private readonly IProteinRepository _proteinRepository;

        public ProteinService(IProteinRepository proteinRepository)
        {
            _proteinRepository = proteinRepository;
        }

        public List<Protein> GetProteins() => _proteinRepository.GetAll();

        public void CreateProtein(Protein protein)
        {
            if (_proteinRepository.Contains(protein))
                throw new InvalidOperationException($"Protein with id = {protein.Id} already registered");
            _proteinRepository.Insert(protein);
        }
    }
}
