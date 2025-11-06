using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;

        public TagService(IMapper mapper, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<TagDTO> GetById(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<TagDTO>(tag);
        }

        public async Task<TagDTO> GetByName(string name)
        {
            var tag = await _tagRepository.GetByNameAsync(name);
            return _mapper.Map<TagDTO>(tag);
        }

        public async Task<IEnumerable<TagDTO>> GetAll()
        {
            var tags = await _tagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagDTO>>(tags);
        }

        public async Task<TagDTO> Create(TagDTO tagDTO)
        {
            var tag = _mapper.Map<Tag>(tagDTO);
            var created = await _tagRepository.CreateAsync(tag);
            return _mapper.Map<TagDTO>(created);
        }

        public async Task<TagDTO> Update(TagDTO tagDTO)
        {
            var tag = _mapper.Map<Tag>(tagDTO);
            var updated = await _tagRepository.UpdateAsync(tag);
            return _mapper.Map<TagDTO>(updated);
        }

        public async Task<bool> Delete(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) return false;

            await _tagRepository.DeleteAsync(tag);
            return true;
        }
    }
}