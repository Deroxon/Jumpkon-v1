const functions = require("firebase-functions");
const admin = require("firebase-admin");
const fetch = require("node-fetch");

// Inicjalizacja Firebase Admin SDK
admin.initializeApp();

// Klucz API ukryty na serwerze
const FIREBASE_API_KEY = "AIzaSyDu6Yxf1QkJHxJrMMWSuxljHVWQUUmWX7c";

// Funkcja HTTP, która będzie pośredniczyć w zapytaniach
exports.getHighScores = functions.https.onRequest(async (req, res) => {
  try {
    // Przykład zapytania do Firebase Database
    const db = admin.database();
    const ref = db.ref("highscores");
    const snapshot = await ref.once("value");

    const highScores = snapshot.val();

    // Zwracamy dane do klienta
    res.status(200).send(highScores);
  } catch (error) {
    res.status(500).send("Błąd podczas pobierania wyników");
  }
});
