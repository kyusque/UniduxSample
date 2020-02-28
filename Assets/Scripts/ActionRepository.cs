using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unidux;
using UniRx;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace App
{
    
[CreateAssetMenu(menuName = "UniduxSample/ActionRepository")]
public class ActionRepository : ScriptableObject
{
    // interfaceのリストの参照を保持
    // このままではエディタ上で編集できない
    [SerializeReference] private List<IAction> _actions = new List<IAction>();

    private void Dispatch(float second)
    {
        var i = 0;
        Observable
            .Timer(TimeSpan.FromSeconds(second), TimeSpan.FromSeconds(second))
            .Take(_actions.Count)
            .Subscribe(x =>
            {
                Unidux.Dispatch(_actions[i]);
                i++;
            });

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ActionRepository))]
    class ActionRepositoryInspector: Editor
    {
        private ActionRepository _repository;
        private SerializedProperty soActions;
        private int[] currentTypeIndexes;
        private int size;
        private Type[] actionInheritants;
        private Dictionary<Type, int> action2index;
        
        private void OnEnable()
        {
            // 今回はtargetを持っておく方がスッキリ書けるのでこれも保持しておく
            _repository = target as ActionRepository;
            
            // _actionの参照
            soActions = serializedObject.FindProperty("_actions");
            
            // IActionを継承しているクラスをアセンブリから全て取り出す
            actionInheritants = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass)
                .Where(x => typeof(UniduxSample.IAction).IsAssignableFrom(x))
                .ToArray();
            
            action2index =  Enumerable.Range(0, actionInheritants.Length).ToDictionary(i => actionInheritants[i]);
            size = soActions.arraySize;
        }

        public override void OnInspectorGUI()
        {
            // 見やすさのためリストのインデント表示は手作りする
            serializedObject.Update();
            EditorGUILayout.LabelField("Action List");
            EditorGUI.indentLevel++;
            
            // リストのサイズ変更の実装
            size = EditorGUILayout.IntField("size", size);
            // SerializedPropertyはアレイになるが、数がsizeより小さければ後ろを削って
            // 大きければ追加して、一つ目の継承クラスのインスタンスを生成、参照に代入する
            while (soActions.arraySize != size)
            {
                if (soActions.arraySize < size)
                {
                    
                    soActions.InsertArrayElementAtIndex(soActions.arraySize);
                    soActions.GetArrayElementAtIndex(soActions.arraySize - 1).managedReferenceValue
                        = Activator.CreateInstance(actionInheritants[0]);
                }
                else
                {
                    soActions.DeleteArrayElementAtIndex(soActions.arraySize - 1);
                }
                
            }
            // このあと、targetを使いたいので更新処理を行う
            // クラスが変わることについてUndoRecodeできないようなのでWithoutUndoにする。
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            
            
            if (soActions.arraySize > 0)
            {
                serializedObject.Update();
                // int[] でクラス情報を保持
                // Popupを使うため
                currentTypeIndexes = 
                    _repository._actions
                    .Select(x => x.GetType())
                    .Select(x => action2index[x])
                    .ToArray();
                EditorGUI.indentLevel++;
            
                for (int i = 0; i < soActions.arraySize; i++)
                {
                    var soItem = soActions.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(soItem,true);
                    EditorGUI.indentLevel++;
                    int currentIndex = EditorGUILayout.Popup("Action", currentTypeIndexes[i], ActionPopupNames);
                    EditorGUI.indentLevel--;
                    if(currentIndex == currentTypeIndexes[i]) continue;
                    // クラス情報が変化したら、新しいインスタンスを作成して代入し直す。
                    soItem.managedReferenceValue = Activator.CreateInstance(actionInheritants[currentIndex]);
                }
            
                EditorGUI.indentLevel--;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }

            EditorGUI.indentLevel--;

            // 3秒ごとにリスト内のActionを実行する。
            if (GUILayout.Button("Dispatch"))
            {
                if (Application.isPlaying)
                {
                    _repository.Dispatch(3);
                }
            }
        }

        private string[] ActionPopupNames => actionInheritants.Select(x => x.ToString()).ToArray();
    }
#endif
}

}
