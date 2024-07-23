using EscapeFromTarkovCheat.Data;
using EscapeFromTarkovCheat.Utils;
using UnityEngine;

namespace EscapeFromTarkovCheat.Feature.ESP
{
    public static class PlayerBones
    {
        public static void DrawSkeleton(GamePlayer gamePlayer)
        {
            if (GameUtils.IsPlayerValid(gamePlayer.Player))
            {
                // Déterminez la couleur du squelette en fonction de la visibilité du joueur
                Color visibleColor = Settings.VisibleSkeletonColor; // Utilisation de la couleur définie dans Settings
                Color hiddenColor = Settings.SkeletonColor; // Utilisation de la couleur définie dans Settings

                // Définir les positions des os
                Vector3 headPos = gamePlayer.GetBonePosition(BoneType.HumanHead);
                Vector3 neckPos = gamePlayer.GetBonePosition(BoneType.HumanNeck);
                Vector3 spinePos = gamePlayer.GetBonePosition(BoneType.HumanSpine3);
                Vector3 pelvisPos = gamePlayer.GetBonePosition(BoneType.HumanPelvis);

                Vector3 leftShoulderPos = gamePlayer.GetBonePosition(BoneType.HumanLCollarbone);
                Vector3 leftUpperArmPos = gamePlayer.GetBonePosition(BoneType.HumanLUpperarm);
                Vector3 leftElbowPos = gamePlayer.GetBonePosition(BoneType.HumanLForearm1);
                Vector3 leftForearmPos = gamePlayer.GetBonePosition(BoneType.HumanLForearm2);
                Vector3 leftHandPos = gamePlayer.GetBonePosition(BoneType.HumanLPalm);

                Vector3 rightShoulderPos = gamePlayer.GetBonePosition(BoneType.HumanRCollarbone);
                Vector3 rightUpperArmPos = gamePlayer.GetBonePosition(BoneType.HumanRUpperarm);
                Vector3 rightElbowPos = gamePlayer.GetBonePosition(BoneType.HumanRForearm1);
                Vector3 rightForearmPos = gamePlayer.GetBonePosition(BoneType.HumanRForearm2);
                Vector3 rightHandPos = gamePlayer.GetBonePosition(BoneType.HumanRPalm);

                Vector3 leftHipPos = gamePlayer.GetBonePosition(BoneType.HumanLThigh1);
                Vector3 leftThigh2Pos = gamePlayer.GetBonePosition(BoneType.HumanLThigh2);
                Vector3 leftKneePos = gamePlayer.GetBonePosition(BoneType.HumanLCalf);
                Vector3 leftCalfPos = gamePlayer.GetBonePosition(BoneType.HumanLCalf);
                Vector3 leftFootPos = gamePlayer.GetBonePosition(BoneType.HumanLFoot);

                Vector3 rightHipPos = gamePlayer.GetBonePosition(BoneType.HumanRThigh1);
                Vector3 rightThigh2Pos = gamePlayer.GetBonePosition(BoneType.HumanRThigh2);
                Vector3 rightKneePos = gamePlayer.GetBonePosition(BoneType.HumanRCalf);
                Vector3 rightCalfPos = gamePlayer.GetBonePosition(BoneType.HumanRCalf);
                Vector3 rightFootPos = gamePlayer.GetBonePosition(BoneType.HumanRFoot);

                // Dessin du squelette
                DrawBoneLineWithVisibilityCheck(headPos, neckPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(neckPos, spinePos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(spinePos, pelvisPos, hiddenColor, visibleColor);

                // Épaules et bras
                DrawBoneLineWithVisibilityCheck(neckPos, leftShoulderPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftShoulderPos, leftUpperArmPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftUpperArmPos, leftElbowPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftElbowPos, leftForearmPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftForearmPos, leftHandPos, hiddenColor, visibleColor);

                DrawBoneLineWithVisibilityCheck(neckPos, rightShoulderPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightShoulderPos, rightUpperArmPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightUpperArmPos, rightElbowPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightElbowPos, rightForearmPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightForearmPos, rightHandPos, hiddenColor, visibleColor);

                // Hanches et jambes
                DrawBoneLineWithVisibilityCheck(pelvisPos, leftHipPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftHipPos, leftThigh2Pos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftThigh2Pos, leftKneePos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftKneePos, leftCalfPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(leftCalfPos, leftFootPos, hiddenColor, visibleColor);

                DrawBoneLineWithVisibilityCheck(pelvisPos, rightHipPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightHipPos, rightThigh2Pos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightThigh2Pos, rightKneePos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightKneePos, rightCalfPos, hiddenColor, visibleColor);
                DrawBoneLineWithVisibilityCheck(rightCalfPos, rightFootPos, hiddenColor, visibleColor);
            }
        }

        private static void DrawBoneLineWithVisibilityCheck(Vector3 start, Vector3 end, Color hiddenColor, Color visibleColor)
        {
            Color color = IsBoneVisible(start, end) ? visibleColor : hiddenColor;
            Render.DrawBoneLine(start, end, 1.5f, color);
        }

        private static bool IsBoneVisible(Vector3 start, Vector3 end)
        {
            Vector3 direction = end - start;
            float distance = direction.magnitude;
            direction.Normalize();

            // Raycast pour vérifier si l'os est visible
            if (Physics.Raycast(start, direction, out RaycastHit hit, distance))
            {
                // Si le raycast touche quelque chose avant l'os, il est caché
                return hit.distance >= distance;
            }

            // Si le raycast ne touche rien, l'os est visible
            return true;
        }
    }
}
