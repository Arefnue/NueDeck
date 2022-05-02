using UnityEngine;

namespace NueGames.NueDeck.Scripts.NueExtentions
{
    public static class GameObjectExtentions
    {
        /// <summary>
        ///     Gets a component attached to the given game object.
        ///     If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

        /// <summary>
        ///     Checks whether a game object has a component of type T attached.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.GetComponent<T>() != null;
    }
}
