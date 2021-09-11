using Kindred.Bowling.Api.Models;
using System.Collections.Generic;

namespace Kindred.Bowling.Api.Services.Framing
{
    public interface IFramingService
    {
        List<Frame> GetFrames(List<int> pinsDowned);
    }
}
