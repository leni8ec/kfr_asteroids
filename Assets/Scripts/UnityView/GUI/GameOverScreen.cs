﻿using TMPro;
using UnityView.Base;

namespace UnityView.GUI {
    public class GameOverScreen : MonoBase {
        public TextMeshProUGUI score;

        private void OnEnable() {
            if (States == null) return;
            score.SetText($"Score\n{States.score.Points.Value}");
        }
    }
}