using RamenGo.Domain.Entities;
using RamenGo.Domain.Repositories;

namespace RamenGo.Application.Services
{
    public class BrothService
    {
        private readonly IBrothRepository _brothRepository;

        public BrothService(IBrothRepository brothRepository)
        {
            _brothRepository = brothRepository;
        }

        public List<Broth> GetBroths() => _brothRepository.GetAll();

        public void CreateBroth(Broth broth)
        {
            if (_brothRepository.Contains(broth))
                throw new InvalidOperationException($"Protein with id = {broth.Id} already registered");
            _brothRepository.Insert(broth);
        }
    }
}
