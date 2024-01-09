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
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activitiy { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                // TODO: Bu kısımı düzeltmek gerekiyor.
                request.Activitiy.Date = DateTime.UtcNow.AddDays(10);
                //Cannot write DateTime with Kind = Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported.

                _context.Activities.Add(request.Activitiy);

                await _context.SaveChangesAsync();
            }
        }

    }
}
