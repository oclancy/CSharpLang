namespace GrpcApiGateway
{
    public interface IGrpcClient
    {
        Task<ResponseModel.PerformanceStatusModel> GetPerformanceStatus(string clientName);
    }
}
