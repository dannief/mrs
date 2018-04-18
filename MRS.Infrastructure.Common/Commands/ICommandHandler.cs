using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Infrastructure.Common.Commands
{
    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
    {
        void Handle(TCommand command);
    }
}
