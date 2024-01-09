using AutoMapper;
using MediatR;
using Reactivities.Domain;
using Reactivities.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactivities.Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activitiy { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activitiy.Id);

                // TODO: Bu kısımı düzeltmek gerekiyor.
                //<Form.Input type='date' placeholder='Date' value={activity.date} name='date' onChange={handleInputChange}/>
                request.Activitiy.Date = DateTime.UtcNow.AddDays(10); // Yukarıdaki satırdan istenilen formatta veri gelmiyor o nedenle geçiçi çözüm olarak bu satır eklendi.
                //Cannot write DateTime with Kind = Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported.
                _mapper.Map(request.Activitiy, activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}