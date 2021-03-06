using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Color { BLACK, WHITE };

public class WorldController : MonoBehaviour {
    World world;

    static bool loadWorldbool = false;

    // Start is called before the first frame update
    private void OnEnable() {

        if (loadWorldbool) {
            loadWorldbool = false;
            createWorldFromSaveFile();
        }

        else {
            createEmptyWorld();
        }
    }

    private void createEmptyWorld() {
        world = new World();

        // Center the camera.
        Camera.main.transform.position = new Vector3(4, 4, Camera.main.transform.position.z);
    }

    #region Saving and loading

    public void newWorld() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        createEmptyWorld();
    }

    public void saveWorld() {
        XmlSerializer serializer = new XmlSerializer(typeof(World));
        TextWriter writer = new StringWriter();

        serializer.Serialize(writer, world);
        writer.Close();

        Debug.Log(writer.ToString());

        PlayerPrefs.SetString("SaveGame00", writer.ToString());
    }

    public void loadWorld() {

        // Reload the scene to reset all the data.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        loadWorldbool = true;
    }

    private void createWorldFromSaveFile() {

        XmlSerializer serializer = new XmlSerializer(typeof(World));
        TextReader reader = new StringReader(PlayerPrefs.GetString("SaveGame00"));

        world = (World)serializer.Deserialize(reader);
        reader.Close();


        // Center the camera.
        Camera.main.transform.position = new Vector3(4, 4, Camera.main.transform.position.z);
    }

    #endregion Saving and loading
}
