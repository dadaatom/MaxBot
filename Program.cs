using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Collections;

using Discord;
using Discord.Net.Providers.WS4Net;
using Discord.WebSocket;
using Discord.Commands;

using MySql.Data;
using MySql.Data.MySqlClient;

using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;

namespace MaxBot_V2
{
    class Program
    {
        MySqlConnection conn;
        Alphabet alpha;
        TranslateService service;
        Random rand;
        IGuildUser user;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        public async Task Start()
        {
            rand = new Random();
            alpha = new Alphabet();

            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                WebSocketProvider = WS4NetProvider.Instance
            });

            string myConnectionString;

            myConnectionString =
                "server=127.0.0.1;" +
                "uid=USERNAME;" +
                "pwd=PASSWORD;" +
                "database=DATABASE;";
            /*
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
                System.Environment.Exit(1);
            }*/

            service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = "GOOGLE_API_KEY_HERE", // your API key, that you get from Google Developer Console
                ApplicationName = "MaxBot" // your application name, that you get form Google Developer Console
            });
            
            client.MessageReceived += (message) =>
            {
                return Task.Run(() =>
                {
                    if (message.Content.Substring(0, 1).Equals("."))
                    {
                        user = (IGuildUser)message.Author;
                        string[] cmdArgs = message.Content.Substring(1, message.Content.Length - 1).Split(' ');

                        if (cmdArgs[0].Equals("help"))
                        {
                            Commands cmd = new Commands();

                            if (cmdArgs.Length > 1)
                            {

                            }
                            else
                            {
                                string toSend = "";
                                for (int i = 0; i < cmd.getCommands().Keys.ToArray().Length; i++)
                                {
                                    toSend += cmd.getCommands().Keys.ToArray()[i] + ", ";

                                }

                                message.Author.SendMessageAsync("```" + toSend.Substring(0, toSend.Length - 2) + ".```");
                            }
                        }
                        else if ((cmdArgs[0].Equals("expand") || cmdArgs[0].Equals("ex") || cmdArgs[0].Equals("x")) && cmdArgs.Length > 1)
                        {
                            string toProcess = combineAfter(1, cmdArgs);
                            string finalStr = "";

                            for (int i = 0; i < toProcess.Length; i++)
                            {

                                if (alpha.getCursive().ContainsKey(toProcess.Substring(i, 1)))
                                {
                                    finalStr = finalStr + ":regional_indicator_" + toProcess.Substring(i, 1).ToLower() + ":";
                                }
                                else
                                {
                                    finalStr = finalStr + toProcess.Substring(i, 1);
                                }

                            }
                            message.Channel.SendMessageAsync(finalStr);

                        }
                        else if ((cmdArgs[0].Equals("curs") || cmdArgs[0].Equals("cursive")) && cmdArgs.Length > 1)
                        {
                            string cursiveText = "";
                            string toProcess = combineAfter(1, cmdArgs);

                            Alphabet alpha = new Alphabet();
                            for (int i = 0; i < toProcess.Length; i++)
                            {
                                if (alpha.getCursive().ContainsKey(toProcess.Substring(i, 1)))
                                {
                                    cursiveText += alpha.getCursive()[toProcess.Substring(i, 1)];
                                }
                                else
                                {
                                    cursiveText += toProcess.Substring(i, 1);
                                }
                            }
                            message.Channel.SendMessageAsync(cursiveText);
                        }
                        else if (cmdArgs[0].Equals("translate") || cmdArgs[0].Equals("tr"))
                        {
                            try
                            {
                                TranslationsListResponse translation = service.Translations.List(combineAfter(2, cmdArgs), cmdArgs[1]).Execute();
                                message.Channel.SendMessageAsync(translation.Translations[0].TranslatedText);
                            }

                            catch (Google.GoogleApiException gooAPI)
                            {
                                Console.WriteLine(gooAPI.StackTrace);
                                message.Channel.SendMessageAsync(cmdArgs[1] + " is not a recognized abbreviated language.");
                            }

                        }
                        else if (cmdArgs[0].Equals("id"))
                        {
                            if (cmdArgs.Length > 1)
                            {

                                Task<string> userID = getUserID((IGuild)message.Channel, cmdArgs[1]);

                                //message.Channel.SendMessageAsync(userID.Result.ToString());
                                if (userID.Result != null)
                                {

                                    message.Channel.SendMessageAsync(combineAfter(1, cmdArgs) + ": " + userID.Result.ToString());
                                }
                                else
                                {
                                    message.Channel.SendMessageAsync(combineAfter(1, cmdArgs) + ": " + "User not found.");
                                }
                            }
                            else
                            {
                                message.Channel.SendMessageAsync(combineAfter(1, cmdArgs) + ": " + "User not found.");
                            }
                        }
                        else if (cmdArgs[0].Equals("languages"))
                        {
                            string toSend = "quack";

                            message.Author.SendMessageAsync(toSend);
                        }
                        else if (cmdArgs[0].Equals("random") || cmdArgs[0].Equals("rand"))
                        {
                            if (cmdArgs.Length == 1)
                            {
                                message.Channel.SendMessageAsync("Generating a number between 1 and 10...");
                            }
                            else if (cmdArgs.Length == 2)
                            {
                                message.Channel.SendMessageAsync("Generating a number between 1 and " + cmdArgs[1] + "...");
                            }
                            else if (cmdArgs.Length > 2)
                            {
                                message.Channel.SendMessageAsync("Generating a number between " + cmdArgs[1] + " and " + cmdArgs[2] + "...");
                            }
                        }
                        //((IGuildUser)message.Author).VoiceChannel.
                        else if (cmdArgs[0].Equals("promote") && cmdArgs.Length == 2 && user.Guild.OwnerId == user.Id)
                        {

                        }
                        else
                        {
                            message.Channel.SendMessageAsync("Command not recognized, type '.help' for help.");
                        }
                    }
                    else if (message.Author.IsBot && message.Author.Username == "MaxBot" && message.Author.Id.ToString().Equals("252196229469962240"))
                    {
                        if (message.Content.IndexOf("Generating") == 0)
                        {
                            string[] commandArr = message.Content.Split(' ');
                            message.DeleteAsync();
                            message.Channel.SendMessageAsync("Generated a " + rand.Next(Int32.Parse(commandArr[4]), Int32.Parse(commandArr[6].Substring(0, commandArr[6].IndexOf(".")))) + ".");
                        }
                    }
                });
            };

            client.Log += (l) => Console.Out.WriteLineAsync(l.ToString());

            await client.LoginAsync(TokenType.Bot, "DISCORD_BOT_TOKEN_HERE");
            await client.StartAsync();

            await Task.Delay(-1);

        }

        private string combineAfter(int startIn, string[] toCombine)
        {
            string afterStr = "";
            for (int i = startIn; i < toCombine.Length; i++)
            {
                afterStr = afterStr + toCombine[i] + " ";
                if (i == toCombine.Length - 1)
                {
                    afterStr = afterStr.Substring(0, afterStr.Length - 1);
                }
            }
            return afterStr;
        }

        private async Task<string> getUserID(IGuild serv, string name)
        {

            var x = serv.GetUsersAsync();
            IReadOnlyCollection<IGuildUser> userList = await x;

            for (int i = 0; i < userList.ToArray().Length; i++)
            {
                if (name.Equals(((IGuildUser)userList.ToArray()[i]).Username))
                {
                    return ((IGuildUser)userList.ToArray()[i]).Id.ToString();
                }

                else if (name.Equals(((IGuildUser)userList.ToArray()[i]).Username))
                {
                    return ((IGuildUser)userList.ToArray()[i]).Id.ToString();
                }
            }
            return null;
        }
        private async Task<IGuildUser> getUser(IGuild serv, string id)
        {
            var x = serv.GetUsersAsync();
            IReadOnlyCollection<IGuildUser> userList = await x;

            for (int i = 0; i< userList.ToArray().Length; i++) {
                if (userList.ToArray()[i].Id.ToString().Equals(id)) {
                    return (IGuildUser)userList.ToArray()[i];
                }
            }

            return null;

        }
    }
    
}

