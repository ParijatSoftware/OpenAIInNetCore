# OpenAIInNetCore
Samples of OpenAI APIs in Net Core. More samples coming soon...

# Samples as of Now
## 1. Instant BedTime Story Generator
Uses OpenAI GPT API to generate a short bedtime story on any given topic. Also uses SignalR to display the response in real-time! 
Notes:
- Update OpenAI Api key in `appsettings.json`
 
![Open AI Sample - BedTime Story Generator](https://github.com/ParijatSoftware/OpenAIInNetCore/assets/9824189/9f41cce6-ad36-4c34-b738-87ffde4d8a39)


## 2. Question and Answer App using Embedding (with your own data)
Uses OpenAI GPT and Embedding API to build a simple Q&A application. For data, right now I am using Winter 2022 Olympics articles. Also uses SignalR to display the response in real-time!. I am using OpenAI example [application](https://github.com/openai/openai-cookbook/blob/main/examples/Question_answering_using_embeddings.ipynb) as a reference. OpenAI example is in Python and this one is in netcore 7. I have also used [OpenAI](https://github.com/OkGoDoIt/OpenAI-API-dotnet) and [SharpToken](https://github.com/dmitry-brazhenko/SharpToken) Nuget Package. Both Nuget packages are community-built.

In this sample application, you can input your own [vector] data as well and do Q&A. Just need some minor changes like change filepath, change in instruction in OpenAIAPIConnecter functions, changes in UI as needed and you are good to go with your own data.
Notes:
- Update OpenAI Api key in `appsettings.json`
- In `Infrastructure/DataFiles` folder, add Winter 2022 Olympics data file (download it from [here](https://cdn.openai.com/API/examples/data/winter_olympics_2022.csv)).

![Open AI Sample - QnA using Embedding](https://github.com/ParijatSoftware/OpenAIInNetCore/assets/9824189/d0931256-4ddc-40a6-828d-183f424a2335)

