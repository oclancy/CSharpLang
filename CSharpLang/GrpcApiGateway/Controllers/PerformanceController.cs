using Grpc.Net.Client;

using GrpcDependencies;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using Monitor = GrpcDependencies.Monitor;

namespace GrpcApiGateway.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {

        private readonly Monitor.MonitorClient factoryClient;
        private readonly IGrpcClient clientWrapper;
        private readonly string serverUrl;

        public PerformanceController(Monitor.MonitorClient
          factoryClient,
          IGrpcClient clientWrapper,
          IConfiguration configuration)
        {
            this.factoryClient = factoryClient;
            this.clientWrapper = clientWrapper;
            serverUrl = configuration["ServerUrl"];
        }

        [HttpGet("factory-client/{count}")]
        public async Task<ResponseModel>
        GetPerformanceFromFactoryClient(int count)      
        {
            var stopWatch = Stopwatch.StartNew();
            var response = new ResponseModel();
            for (var i = 0; i < count; i++)
            {
                var grpcResponse =await factoryClient.GetPerformanceAsync(new PerformanceStatusRequest { ClientName = $"client {i + 1}" });

                response.PerformanceStatuses.Add(new ResponseModel.PerformanceStatusModel
                {
                    CpuPercentageUsage = grpcResponse.CpuPercentageUsage,
                    MemoryUsage = grpcResponse.MemoryUsage,
                    ProcessesRunning = grpcResponse.ProcessesRunning,
                    ActiveConnections = grpcResponse.ActiveConnections
                });
            }
            response.RequestProcessingTime = stopWatch.ElapsedMilliseconds;
            return response;
        }

        [HttpGet("client-wrapper/{count}")]
        public async Task<ResponseModel> GetPerformanceFromClientWrapper(int count)
        {
            var stopWatch = Stopwatch.StartNew();
            var response = new ResponseModel();
            for (var i = 0; i < count; i++)
            {
                var grpcResponse = await clientWrapper.GetPerformanceStatus($"client {i + 1}");
                response.PerformanceStatuses.Add(grpcResponse);
            }
            response.RequestProcessingTime = stopWatch.ElapsedMilliseconds;
            return response;
        }

        [HttpGet("initialized-client/{count}")]
        public async Task<ResponseModel> GetPerformanceFromNewClient(int count)
        {
            var stopWatch = Stopwatch.StartNew();
            var response = new ResponseModel();
            for (var i = 0; i < count; i++)
            {
                using var channel = GrpcChannel.ForAddress(serverUrl);
                var client = new Monitor.MonitorClient(channel);
                var grpcResponse = await client.GetPerformanceAsync(new PerformanceStatusRequest { ClientName = $"client {i + 1}" });
                response.PerformanceStatuses.Add(new ResponseModel.PerformanceStatusModel
                {
                    CpuPercentageUsage = grpcResponse.CpuPercentageUsage,
                    MemoryUsage = grpcResponse.MemoryUsage,
                    ProcessesRunning = grpcResponse.ProcessesRunning,
                    ActiveConnections = grpcResponse.ActiveConnections
                });
            }
            response.RequestProcessingTime = stopWatch.ElapsedMilliseconds;
            return response;
        }
    }
}
