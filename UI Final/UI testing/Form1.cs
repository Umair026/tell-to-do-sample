using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Data.SqlClient;

namespace UI_testing
{
    public partial class Form1 : Form
    {
        String var1;

        SqlConnection con = new SqlConnection(@"data source= CKS\SQLEXPRESS;database= agent; integrated security=SSPI ");
        SqlCommandBuilder cmdBuilder;
        SpeechSynthesizer synth = new SpeechSynthesizer();
        PromptBuilder promptb = new PromptBuilder();
        SpeechRecognitionEngine recognition = new SpeechRecognitionEngine();
        Boolean wake = true;

        Choices arraylist = new Choices();

        public Form1()
        {
           // con.ConnectionString = @"data source= CKS\SQLEXPRESS;database= agent; integrated security=SSPI ";
            DataSet ds = new DataSet();
            synth.SelectVoiceByHints(VoiceGender.Female);
            synth.Rate = -2;
            synth.Volume = 100;
            synth.Speak("Hello!,  My name is Quad-Bot!, Welcome to my Arena!, please enter your name  ");

            arraylist.Add(new string[] { "Good Morning", "Office", "Exit Web Camera", "Exit Skype", "Exit wordpad", "Skype", "Aliza", "David", "BasketBall", "Badminton", "FootBall", "Cricket", "Robotics", "Computer Science", "Aritificial Intelligence", "Which Sports Do You Like?", "Lets Talk About SomeThing", "System Video", "System Images", "Downloads", "Wikipedia", "Twitter", "Google", "Hotmail", "Facebook", "Youtube", "Do you Have Emotions?", "What Are Your Future Plans?", "When Is Your Birthday", "Thank you", "How Is Your Day", "Who Are You?", "What Are Your Future Plan?", "Are You There?", "When Were You Developed?", "What Is My Name", "How Do You Look Like", "Online Shopping Websites", "documents", "fire", "hi", "web camera", " what about you?", "can you hear me?", "program files", "what time is it?", "wordpad", "calculator", "system configuration", "what is today?", "WHAT IS MY LOCATION", "Shutdown", "wake up", "offline", "c drive", "what is my ip address?", "close" });
            Grammar gr = new Grammar(new GrammarBuilder(arraylist));

            try
            {
                recognition.RequestRecognizerUpdate();
                recognition.LoadGrammar(gr);
                recognition.SpeechRecognized += recognition_SpeechRecognized;
                recognition.SetInputToDefaultAudioDevice();
                recognition.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
            InitializeComponent();
        }

        

        public void say(String h)
        {
             
            synth.Speak(h);
            BottextBox2.AppendText(h + "\n");
        }


        private void recognition_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            string r = e.Result.Text;
            if (r=="wakeup")
            {
                pictureBox4.Show();
               wake = true;
                statuslabel3.Text = "status : awake";
            }
            if (r == "offline")
            {
                pictureBox4.Hide();
                wake = false;
                statuslabel3.Text = "status : sleep";
            }
            // wake = true; while (dwr.Read())
            {

               


            }
            //no need of wakeup here
             
            //SqlCommand cmd = new SqlCommand("select say from internetAccess where say='"+r+"' ", con);
            SqlCommand cmd =con.CreateCommand();
            cmd.CommandText = "select say from internetAccess where say='"+r+"'";
            con.Open();
              //  SqlDataReader dwr = cmd.ExecuteReader();
                //while (dwr.Read()) {
                  //  var1 = dwr.GetString(0);
                        // }
                con.Close();
            var1= cmd.CommandText;
                if (var1.Equals("The branch of physics that deals with the motion of material objects. The term mechanics generally refers to the motion of large objects, whereas the study of motion at the level of the atom or smaller is the domain of quantum mechanics."))         //good morning
                {
                    say("The branch of physics that deals with the motion of material objects. The term mechanics generally refers to the motion of large objects, whereas the study of motion at the level of the atom or smaller is the domain of quantum mechanics." + NametextBox1.Text + " And Your? ? ");

                }
                if ( r.Equals(var1))
                {
                    say("yes "+NametextBox1.Text+", i can hear you clearly and loud");

                }
                if (var1.Equals("program file"))    //program files
                {
                    Process.Start(@"c:\Program Files");
                }
                if (var1.Equals("Hello"))     // hello
                {
                    say("Hello! " + " " + NametextBox1.Text.ToString() + "  Good morning, How are you ?");
                }
                if (var1.Equals( "hi"))      //hi
                {
                    say("I am good" + " " + NametextBox1.Text.ToString());
                }
                if (var1.Equals("My name is David. What is your good name?"))    //My name is David. What is your good name?"
                {
                    say("The current time is " + DateTime.Now.ToString("h:mm tt"));
                }
                if (r == "welcome")           //welcome
                 {
                    say("Today  is " + DateTime.Now.ToString("M/d/yyyy"));
                }
                if (r == "WHAT IS MY LOCATION")   //location
                {
                    say("Your cuurent location is ");
                    Process.Start("https://www.google.com/maps/@24.9151239,67.0915457,16z");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (r == "Shutdown")      //shutdown
                {
                    say("Thank you!" + " " + "See You Soon " + NametextBox1.Text.ToString() + " have a good day, good bye");
                    Application.Exit();
                }
                //if (r == "calculator")
                //{
                //    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Accessories\Calculator");

                //}
                if (r.Equals("c drive"))    //c drive
                {
                    Process.Start(@"c:\\");
                }
                if (r == "what is my ip address?")     // ip adress
                {
                    string hostname = Dns.GetHostName();
                    textBox1.AppendText(hostname);
                    string myip = Dns.GetHostByName(hostname).AddressList[0].ToString();
                    textBox1.AppendText(myip);
                    say("hostname is " + hostname + "\n");
                    say("ip address is " + myip + "\n");
                }
                if (r.Equals(var1))        //wordpad
                {
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Accessories\WordPad");

                }
                if (r == "Exit wordpad")             //exit wordpad
                {
                    foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())
                    {
                        if (pr.ProcessName == "wordpad")
                        {
                            pr.Kill();
                        }
                    }

                }

                if (r.Equals(var1))
                {
                    textBox1.AppendText(System.Environment.OSVersion.Platform.ToString());
                    say("platform is " + System.Environment.OSVersion.Platform.ToString() + "\n");
                    textBox1.AppendText(System.Environment.OSVersion.ServicePack.ToString());
                    say("Servicepack is " + System.Environment.OSVersion.ServicePack.ToString() + "\n");
                    textBox1.AppendText(System.Environment.OSVersion.Version.ToString());
                    say("version is " + System.Environment.OSVersion.Version.ToString() + "\n");
                    textBox1.AppendText(System.Environment.OSVersion.VersionString.ToString());
                    say("version string is " + System.Environment.OSVersion.VersionString.ToString() + "\n");

                }
                if (r.Equals(var1))         //open camera
                {
                    say("Opening Web Camera");
                    Process.Start(@"C:\Program Files (x86)\ArcSoft\WebCam Companion 4\uMCEDVDPlayer.exe");
                }

                if (r == "Exit Web Camera")       //exit camera
                {
                    foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())
                    {
                        if (pr.ProcessName == "uMCEDVDPlayer")
                        {
                            pr.Kill();
                        }
                    }

                }
                if (r == "Skype")     //skype
                {
                    say("Opening Skype");
                    Process.Start(@"C:\Program Files (x86)\Skype\Phone\Skype.exe");
                }


                if (r == "Exit Skype")  //exit skype
                {
                    foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())
                    {
                        if (pr.ProcessName == "Skype")
                        {
                            pr.Kill();
                        }
                    }

                }

                if (r == "Office")     //office
                {
                    say(" Opening Microsoft Office ");
                    Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Office 2013");
                }


                if (r == "How Do You Look Like")
                {
                    say("Like  A Thousand  Line Of Code!");
                }
                if (r == "What Is My Name")   //name
                {
                    say(" Your Name Is " + NametextBox1.Text);
                }

                if (r == "When Were You Developed?")
                {
                    say(" I Was Developed By M.A 3 In 2015  ");
                }
                if (r == "Are You There?")
                {
                    say("Yes!"+NametextBox1.Text+" I am Here Just Waiting For Your Next Instruction");
                }
                if (r == "What Are Your Future Plan?")
                {
                    say(" My Plan is To Become Smarter Then Human And Immortal");
                }


                if (r == "Who Are You?")
                {
                    say("My Name Is Quad Bot! , I am An Autonomous Artificial Intelligence Being! , I am Design To Provide Ease To My User");
                }

                if (r == "How Is Your Day")
                {
                    say(" Any Day With You , Will Be Great");
                }

                if (r == "Thank you")    //thanku
                {
                    say(" Do not Mention");
                }
                if (r == "When Is Your Birthday")   //birthday
                {
                    say("When You Active Me!");
                }
                if (r == "What Are Your Future Plans?")
                {
                    say("I Plan To Become Smarter Then Human And Immortal");
                }
                if (r == "Do you Have Emotions?")
                {
                    say("I dont Have Emotions! , Because I am Atrificial Intelligent Bot ");
                }
                if (r == "Lets Talk About SomeThing")
                {
                    say("What Do You Want To Talk ABout ");
                    say("I Have General Knowledge About Artificial Intelligence! , Computer Science , Robotics");
                }
                if (r.Equals(var1))  //ai
                {
                    Process.Start("https://en.wikipedia.org/wiki/Artificial_intelligence");
                    say("Artificial intelligence (AI) is the intelligence exhibited by machines or software. It is also the name of the academic field of study which studies how to create computers and computer software that are capable of intelligent behavior");
                }
                if (r == "Robotics")    //robotics
                {
                    Process.Start("https://en.wikipedia.org/wiki/Robotics");
                    say("Robotics is the branch of mechanical engineering, electrical engineering, electronic engineering and computer science that deals with the design, construction, operation, and application of robots");
                }
                if (r == "Computer Science")   //computer science
                {
                    Process.Start("https://en.wikipedia.org/wiki/Computer_science");
                    say("Computer science is the scientific and practical approach to computation and its applications.");
                }
                if (r == "Which Sports Do You Like?")
                {
                    say("I Like FootBall , My Favourite Team Is Real Madrid , Which Sport Do You Like? ");
                }
                if (r == "BasketBall")      //basketball
                {
                    say("It Is a Good Game But I Have No Interest In BasketBall ");
                }
                if (r == "Cricket")      //cricket
                {
                    say("It Is a Nice Game But I Have No Interest In Cricket ");
                }
                if (r == "Badminton")    //badminton
                {
                    say("It Is a great Game But I Have No Interest In Badminton ");
                }
                if (r == "FootBall")     //football
                {
                    say("I Am Glad ,  We Have SomeThing In Common ");
                }

                if (r == "David")   //david
                {
                    synth.SelectVoiceByHints(VoiceGender.Male);
                    say("Yes" + NametextBox1.Text + "I am here I am just waiting For your Instruction");
                }
                if (r == "Aliza")     //aliza
                {
                    synth.SelectVoiceByHints(VoiceGender.Female);
                    say("Yes" + NametextBox1.Text + " I am also Here");
                }


                if (r == "documents")   //documents
                {
                    Process.Start(@"C:\Users\INFO\Documents");
                }
                if (r == "Online Shopping Websites")    //online shoping websites
                {
                    say("Searching Shopping Websites ");
                    Process.Start("https://www.daraz.pk");
                    Process.Start("http://www.kaymu.pk");
                    Process.Start("http://www.shopdaily.pk");
                    Process.Start("http://www.symbios.pk");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (var1.Equals("Facebook"))         //facebook
                {
                    say("Searching Facebook ");
                    Process.Start("http://www.facebook.com");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (var1.Equals("Youtube"))     //youtube
                {
                    say("Searching Youtube ");
                    Process.Start("https://www.youtube.com");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (var1.Equals("Google"))    //google
                {
                    say("Searching Google ");
                    Process.Start("http://www.google.com");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (r == "Hotmail")   //hotmail
                {
                    say("Searching Hotmail ");
                    Process.Start("http://www.hotmail.com");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (r == "Twitter")   //twitter
                {
                    say("Searching Twitter ");
                    Process.Start("https://twitter.com/?lang=en");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (r == "Wikipedia")   //wikipedia
                {
                    say("Searching Wikipedia ");
                    Process.Start("https://en.wikipedia.org/wiki/Main_Page");
                    say("here you go " + NametextBox1.Text.ToString());
                }
                if (r == "Downloads")   //downloads
                {
                    say("Opening Downloads ");
                    Process.Start(@"C:\Users\INFO\Downloads");
                    say("here is your Downloads " + NametextBox1.Text.ToString());
                }
                if (r == "System Images")      //images
                {
                    say("Opening System Images ");
                    Process.Start(@"C:\Users\INFO\Pictures\Camera Roll\picnic");
                    say("here is your Pictures " + NametextBox1.Text.ToString());
                }
                if (r == "System Video")   //video
                {
                    say("Opening Videos ");
                    Process.Start(@"C:\Users\Public\Videos");
                    say("here is your Videos " + NametextBox1.Text.ToString());
                }



                if (r == "fire")    //fire
                {

                    foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())
                    {
                        if (pr.ProcessName == "explorer")
                        {
                            pr.Kill();
                        }
                    }
                }
                try
                {

                    if (r == "close")   //close
                    {

                        foreach (System.Diagnostics.Process pr in System.Diagnostics.Process.GetProcesses())
                        {
                            if (pr.ProcessName == "chrome")
                            {
                                pr.Kill();
                            }
                        }
                    }
                }catch(Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }


            

            WeAsktextBox3.AppendText(r + "\n");
        }
    


        private void Form1_Load(object sender, EventArgs e)
        {
            string name;
            name = NametextBox1.Text.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void NametextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
