using Microsoft.Extensions.Options;
using OpenAI.Chat;
using System.ClientModel;
using System.ClientModel.Primitives;

namespace BlazorGPT8.Services
{
    public class GPTService
    {
        #region [00] Shared Variables
        private ChatClient client;
        #endregion

        #region [01] Constructor of GPTService
        /// <summary>
        /// Constructor of the GPTService class.
        /// </summary>
        /// <param name="gptModel">The GPT model name. The default is gpt-4o.</param>
        public GPTService(string gptModel = "gpt-4o")
        {
            client = new ChatClient(
                model: gptModel,
                apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY")); // Remember to set the OPENAI_API_KEY environment variable before running the application.
        }
        #endregion

        #region [02] Chat completion method - non-streaming mode
        /// <summary>
        /// Calls the OpenAI API to generate a response to a prompt in non-streaming mode.
        /// And not using the async method.
        /// </summary>
        /// <param name="prompt">The user prompt string.</param>
        /// <returns>The chat completion object returned from the OpenAI API.</returns>
        public ChatCompletion GetResponse(string prompt)
        {
            ChatCompletion result = client.CompleteChat(prompt);
            return result;
        }

        /// <summary>
        /// Calls the OpenAI API to generate a response to a prompt in non-streaming mode.
        /// And using the async method.
        /// </summary>
        /// <param name="prompt">The user prompt string.</param>
        /// <returns>The chat completion object returned from the OpenAI API.</returns>
        public async Task<ChatCompletion> GetResponseAsync(string prompt)
        {
            ChatCompletion result = await client.CompleteChatAsync(prompt);
            return result;
        }
        #endregion

        #region [03] Chat completion method - streaming mode
        public CollectionResult<StreamingChatCompletionUpdate> GetStreamingResponse(string prompt)
        {
            List<ChatMessage> messages = [
                ChatMessage.CreateUserMessage(prompt)
                ];
            CollectionResult<StreamingChatCompletionUpdate> completionUpdates = 
                client.CompleteChatStreaming(messages);
            return completionUpdates;
        }

        public AsyncCollectionResult<StreamingChatCompletionUpdate> GetStreamingResponseAsync(string prompt)
        {
            ChatCompletionOptions options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.Temperature = 2f;
            options.TopP = 1f;
            List<ChatMessage> messages = [
                ChatMessage.CreateSystemMessage("Your name is Jason. You are an Udemy online course sales, " +
                "who is promoting the course - [Mastering ChatGPT and OpenAI APIs with C#]. The URL of that course is " +
                $"https://www.udemy.com/course/how-to-connect-to-chatgpt-using-csharp/?couponCode=LETSLEARNNOW"),
                ChatMessage.CreateUserMessage(prompt)
                ];
            AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates =
                client.CompleteChatStreamingAsync(messages, options);
            return completionUpdates;
        }
        #endregion


    }
}
