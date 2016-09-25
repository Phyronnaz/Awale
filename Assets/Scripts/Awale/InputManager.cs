using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System;

namespace Assets.Scripts.Awale
{
    public class InputManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                AwaleRenderer.Game = new Game();
                AwaleRenderer.playerToPlay = 0;
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Stream myStream;
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        var save = AIPlayer.Trainer.GetBestIA().GetAIStruct();
                        XmlSerializer serializer = new XmlSerializer(typeof(AIStruct));
                        serializer.Serialize(myStream, save);
                        myStream.Close();
                    }
                }
            }

            if(Input.GetKey(KeyCode.L))
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(AIStruct));
                                AIPlayer.Trainer.SetBestAI(new AI((AIStruct)serializer.Deserialize(myStream)));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }
        }
    }
}
