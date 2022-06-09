using Grpc.Net.Client;

using GrpcDependencies;

namespace GrpcApiGateway
{
    public class GrpcClient : IGrpcClient, IDisposable
    {
        private readonly string serverUri;
        private GrpcChannel channel;
        private bool disposedValue;

        public GrpcClient(string serverUri)
        {
            this.serverUri = serverUri;
            channel = GrpcChannel.ForAddress(serverUri);
        }

        public async Task<ResponseModel.PerformanceStatusModel> GetPerformanceStatus(string clientName)
        {

            var client = new GrpcDependencies.Monitor.MonitorClient(channel);
            var response = await client.GetPerformanceAsync(new PerformanceStatusRequest
            {
                ClientName = clientName
            });

            return new ResponseModel.PerformanceStatusModel
            {
                CpuPercentageUsage = response.CpuPercentageUsage,
                MemoryUsage = response.MemoryUsage,
                ProcessesRunning = response.ProcessesRunning,
                ActiveConnections = response.ActiveConnections
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    channel.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
