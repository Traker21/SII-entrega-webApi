using Microsoft.EntityFrameworkCore;

namespace WebApiProvincia
{
    public class StartUp
    {
        public StartUp(IConfiguration configuracion)
        {
            Configuracion = configuracion;
        }

        public IConfiguration Configuracion { get; }






        public void ConfigureServices(IServiceCollection services)
        {  
            services.AddCors();
            services.AddRazorPages();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
          
           



        }





        //////////////////////


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder
      .WithOrigins("http://localhost:4200") 
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials()
  );

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           

        }

    }



}
