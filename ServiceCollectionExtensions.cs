using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using StudyAssistant.KernelAgents;
using StudyAssistant.ViewModels;
using System;

namespace StudyAssistant
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddKernel().Services.AddOpenAIChatCompletion(modelId: "gpt-4o-mini", apiKey: Environment.GetEnvironmentVariable("OPENAI_KEY")!);
            collection.AddSingleton<TutorAgent>();
            collection.AddSingleton<MainWindowViewModel>();
        }
    }
}
