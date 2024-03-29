﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using KerryShaleFanPage.Server.Interfaces.BusinessLogic;

namespace KerryShaleFanPage.Server.Services
{
    public class ScopedPodcastBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<ScopedPodcastBackgroundService> _logger;  // TODO: Implement logging!

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public ScopedPodcastBackgroundService(ILogger<ScopedPodcastBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);

            _logger.LogInformation("Scoped Podcast Background Service is started.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Scoped Podcast Background Service is running.");

            await Task.Run(async () =>
            {
                await DoWorkAsync(cancellationToken);
            }, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Scoped Podcast Background Service is stopping.");

            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// See: https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service
        /// </summary>
        /// <param name="cancellationToken"></param>
        private async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Scoped Podcast Background Service is working.");

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var podcastBusinessLogicService = scope.ServiceProvider.GetRequiredService<IPodcastBusinessLogicService>();
                await podcastBusinessLogicService.DoWorkAsync(cancellationToken);
            }
            catch (OperationCanceledException ex) when (cancellationToken.IsCancellationRequested)
            {
                var cancelledException = ex;  // TODO: Log exception!
            }
            catch (OperationCanceledException ex)
            {
                var cancelledException = ex;  // TODO: Log exception!
            }
            catch (Exception ex)
            {
                var exception = ex;  // TODO: Log exception!
            }
        }
    }
}
