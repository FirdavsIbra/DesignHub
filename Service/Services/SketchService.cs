using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace Service.Services
{
    public class SketchService: ISketchService
    {
        private readonly ISketchRepository _sketchRepository;
        public SketchService(ISketchRepository sketchRepository)
        {
            _sketchRepository = sketchRepository;
        }

        public async Task AddAsync(ISketch sketch)
        {
            var sketches = await _sketchRepository.GetAllAsync();
            var existingSketch = sketches.FirstOrDefault(x => x.UserId == sketch.UserId);
            if (existingSketch != null)
            {
                sketch.Id = existingSketch.Id;
                await _sketchRepository.Update(sketch);
            }
            else
            {
                await _sketchRepository.AddAsync(sketch);
            }
        }

        public async Task<ISketch> GetByUserId(int userId)
        {
           return await _sketchRepository.GetByUserId(userId);
        }
    }
}
