using Hints;
using MEC;
using Mirror;
using Nebuli.API.Features.Player;
using Nebuli.API.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebuli.API.Features
{
    public class CustomHintManager : MonoBehaviour
    {
        internal Dictionary<CustomHint, CoroutineHandle> _customHints = new();
        internal NebuliPlayer player;
        internal StringBuilder _builder = new();
        internal float _counter;

        public void Update()
        {
            try
            {
                _counter += Time.deltaTime;
                if (_counter < 0.5f)
                    return;
                PrintHud();
                _counter = 0;
            }
            catch (Exception e)
            {
                Log.Error($"A error occured while handling custom hints! Full error --> \n{e}");
            }    
        }


        public void AddHint(string message, float duration = 5f)
        {          
            try
            {
                CustomHint newHint = new(message, duration);
                _builder.Append(message);
                _customHints.Add(newHint, Timing.CallDelayed(duration, () => _customHints.Remove(newHint)));
            }
            catch(Exception e)
            {
                Log.Error($"A error occured while handling custom hints! Full error --> \n{e}");
            }
        }

        public void AddHint(CustomHint customHint)
        {
            try
            {
                _builder.Append(customHint.Content);
                _customHints.Add(customHint, Timing.CallDelayed(customHint.Duration, () => _customHints.Remove(customHint)));
            }
            catch (Exception e)
            {
                Log.Error($"A error occured while handling custom hints! Full error --> \n{e}");
            } 
        }

        public async void PrintHud()
        {
            try
            {
                List<CustomHint> hintsToPrint;
                lock (_customHints)
                {
                    hintsToPrint = new List<CustomHint>(_customHints.Keys);
                }

                string hudMessage = await Task.Run(() =>
                {
                    _builder.Clear();
                    foreach (CustomHint message in hintsToPrint)
                    {
                        _builder.AppendLine(message.Content);
                    }
                    return _builder.ToString();
                });

                player.ReferenceHub.hints.Show(new TextHint(hudMessage, new HintParameter[0], null, durationScalar: 1));
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while handling custom hints! Full error --> \n{e}");
            }
        }


        public void Clear()
        {
            try
            {
                _customHints.Clear();
                _builder.Clear();
            }
            catch (Exception e)
            {
                Log.Error($"A error occured while handling custom hints! Full error --> \n{e}");
            }
        }

        private void OnDestroy()
        {
            try
            {
                Clear();
            }
            catch (Exception e)
            {
                Log.Error($"A error occured while handling custom hints! Full error --> \n{e}");
            }
        }
    }
}

