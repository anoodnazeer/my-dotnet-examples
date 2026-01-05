using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Service.JobProvider.Dto;
using Domain.Service.JobProvider.Interfaces;

namespace Domain.Service.JobProvider
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _repository;
        private readonly IMapper _mapper;

        public InterviewService(IInterviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> ScheduleInterviewAsync(InterviewDto interviewDto)
        {
            var interview = _mapper.Map<Interview>(interviewDto);
            return await _repository.ScheduleInterviewAsync(interview);
        }

        public async Task<List<InterviewDto>> GetAllScheduledInterviewsAsync()
        {
            return await _repository.GetAllScheduledInterviewsAsync();
        }
        public async Task<InterviewDto?> GetInterviewByIdAsync(Guid id)
        {
            var interview = await _repository.GetInterviewByIdAsync(id);
            return interview == null ? null : _mapper.Map<InterviewDto>(interview);
        }

        public async Task<bool> UpdateInterviewAsync(Guid id, InterviewDto updatedInterview)
        {
            var interview = _mapper.Map<Interview>(updatedInterview);
            return await _repository.UpdateInterviewAsync(id, interview);
        }

        public async Task<bool> PatchInterviewAsync(Guid id, string time)
        {
            return await _repository.PatchInterviewAsync(id, time);
        }

        public async Task<bool> UpdateInterviewStatusAsync(Guid id, string status)
        {
            return await _repository.UpdateInterviewStatusAsync(id, status);
        }

        public async Task<bool> DeleteInterviewAsync(Guid id)
        {
            return await _repository.DeleteInterviewAsync(id);
        }
    }
}
