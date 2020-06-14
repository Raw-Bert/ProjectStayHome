using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace TextBoxSystem
{
    public enum MovementType
    {
        None,
        Breath
    }
    public class TextMovement : MonoBehaviour
    {

        [Tooltip("how the text is suppose to move")]
        public MovementType movement;
        bool hasTextChanged = false;
        TMP_Text text;

        void Start()
        {
            text = GetComponent<TMP_Text>() ?? gameObject.AddComponent<TMP_Text>();
            StartCoroutine(Movement());
        }
        void OnEnable()
        {
            // Subscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }

        string laststr;
        void ON_TEXT_CHANGED(Object obj)
        {
            if (obj == text)
                if (laststr == text.text)
                    hasTextChanged = true;
        }
        // Update is called once per frame after all updates
        IEnumerator Movement()
        {

            text.ForceMeshUpdate();

            TMP_TextInfo textInfo = text.textInfo;

            // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
            TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

            hasTextChanged = true;

            while (true)
            {
                // Allocate new vertices 
                if (hasTextChanged)
                {
                    // Update the copy of the vertex data for the text object.
                    cachedMeshInfo = textInfo.CopyMeshInfoVertexData();

                    hasTextChanged = false;
                }

                int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                //TODO: MY CODE HERE
                switch (movement)
                {
                    case MovementType.None:
                        yield return new WaitForSeconds(0.25f);
                        continue;
                    case MovementType.Breath:
                        Breath(ref textInfo, ref cachedMeshInfo);
                        break;
                }
                //MY CODE END

                try
                {
                    //  // Push changes into meshes
                    //  for (int i = 0; i < textInfo.meshInfo.Length; i++)
                    //  {
                    //      textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    //      text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
                    //  }

                    // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                    text.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);

                }
                catch { print("Text movement failed"); }

                yield return new WaitForSeconds(0.1f);

            }
        }

        /// <summary>
        /// Callback sent to all game objects before the application is quit.
        /// </summary>
        void OnApplicationQuit()
        {
            StopCoroutine(Movement());
        }

        float lerpPercent = 0, speed = 10;
        bool posi = true;
        void Breath(ref TMP_TextInfo info, ref TMP_MeshInfo[] originalVerts)
        {
            lerpPercent += speed * (posi ? Time.deltaTime : -Time.deltaTime);
            if (lerpPercent > 1)
            {
                lerpPercent = 1;
                posi = false;
            }
            else if (lerpPercent < 0)
            {
                lerpPercent = 0;
                posi = true;
            }

            for (int i = 0; i < info.characterCount; i++)
            {
                // Skip characters that are not visible and thus have no geometry to manipulate.
                if (!info.characterInfo[i].isVisible)
                    continue;

                // Get the index of the material used by the current character.
                int materialIndex = info.characterInfo[i].materialReferenceIndex;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = info.characterInfo[i].vertexIndex;
                // Get the cached vertices of the mesh used by this text element (character or sprite).
                Vector3[] sourceVertices = originalVerts[materialIndex].vertices;

                Vector3[] destinationVertices = info.meshInfo[materialIndex].vertices;
                //info.meshInfo[materialIndex];
                destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0];
                destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1];
                destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2];
                destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3];

                //top left
                destinationVertices[vertexIndex + 1] = math.lerp(
                    destinationVertices[vertexIndex + 1] + new Vector3(1.1f, -1.1f, 0),
                    destinationVertices[vertexIndex + 1] + new Vector3(-1, 1, 0),
                    lerpPercent);
                //top right
                destinationVertices[vertexIndex + 2] = math.lerp(
                    destinationVertices[vertexIndex + 2] + new Vector3(-1.1f, -1.1f, 0),
                    destinationVertices[vertexIndex + 2] + new Vector3(1, 1, 0),
                    lerpPercent);
                //destinationVertices[vertexIndex + 1] = ;
                //destinationVertices[vertexIndex + 2] += new Vector3(speed,speed, 0);
                //destinationVertices[vertexIndex + 3] += new Vector3(speed,speed, 0);

            }
        }
    }
}