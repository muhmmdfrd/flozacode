using Microsoft.Extensions.DependencyInjection;

namespace Flozacode.Repository
{
    public static class FlozaRepoExtension
    {
		/// <summary>
		/// This method is required added in startup.cs or program.cs for using FlozaRepo
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddFlozaRepo(this IServiceCollection services)
		{
			services.AddScoped(typeof(IFlozaRepo<,>), typeof(FlozaRepo<,>));
			return services;
		}
	}
}
