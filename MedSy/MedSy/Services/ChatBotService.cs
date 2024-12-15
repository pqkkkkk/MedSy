using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.UI.Xaml;

namespace MedSy.Services
{
    public class ChatBotService
    {
        private readonly string apiKey;
        private Kernel kernel;
        private IChatCompletionService chatCompletionService;
        public ChatBotService()
        {
            apiKey = "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";
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddOpenAIChatCompletion(
                modelId: "gpt-3.5-turbo",
                apiKey: apiKey
                );
            kernel = kernelBuilder.Build();
            chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        }
        public async Task<ChatMessageContent> GetChatCompletion(string prompt)
        {
            ChatHistory history = new ChatHistory();
            history.AddUserMessage(prompt);

            try
            {
                return await chatCompletionService.GetChatMessageContentAsync(
                    chatHistory: history,
                    kernel: kernel
                );
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting chat completion", ex);
            }
        }
    }
}
