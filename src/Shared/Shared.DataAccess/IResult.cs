using Shared.DataAccess.Common;

namespace Shared.DataAccess;

public interface IResult <TError> where TError : Error
{
    TError Error { get; }
    bool IsSuccess { get; }
}