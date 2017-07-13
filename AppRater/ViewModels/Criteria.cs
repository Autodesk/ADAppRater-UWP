using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

using AppRater.Services;

namespace AppRater
{
    public class Criteria
    {
        public const String KEY_MIN_DAY_AFTER_FIRST_LAUNCH = "min_day_after_first_launch";
        public const String KEY_TIME_GAP_BETWEEN_REMINDER_ME = "time_gap_between_reminder_me";

        private const String FIRST_LAUNCH_TIME = "AppRater#462@2_first_time_launch";
        private const String ALREADY_ASK_FOR_RATING = "already_ask_for_rating";
        private const String REMIND_ME_LATER_CLICK = "remind_me_later_click";
        private const String REMIND_ME_LATER_CLICK_TIME = "remind_me_later_click_time";

        //key prefix 
        private const String CRITERIA_PREFIX = "AppRater#462@2_Criteria_";
        private const String STATUS_PREFIX = "AppRater#462@2_Status_";

        private static HashSet<String> keys = new HashSet<string>();

        //group the key into groups
        private static Dictionary<String, int> keyGroup = new Dictionary<string, int>();
        
        private static Dictionary<string, int> defaultCriteria = new Dictionary<string, int>
        {
            //if no value from LocalSetting, then use this default one
            { CRITERIA_PREFIX+KEY_TIME_GAP_BETWEEN_REMINDER_ME, 5},
            { CRITERIA_PREFIX+KEY_MIN_DAY_AFTER_FIRST_LAUNCH, 1}
        };

        public static void PopUpAfterXDays(int xDays)
        {
            defaultCriteria[CRITERIA_PREFIX + KEY_MIN_DAY_AFTER_FIRST_LAUNCH] = xDays;
        }

        public static void SetReminderMeGap(int xDays)
        {
            defaultCriteria[CRITERIA_PREFIX + KEY_TIME_GAP_BETWEEN_REMINDER_ME] = xDays;
        }

        public static void SetEventCriteria(String key, int v, int initCount = 0)
        {
            keys.Add(key);

            PreferencesUtil.SetInteger(CRITERIA_PREFIX + key, v);

            //initStatus
            if (!PreferencesUtil.HasKey(STATUS_PREFIX + key))
            {
                PreferencesUtil.SetInteger(STATUS_PREFIX + key, initCount);
            }
        }

        public static void GroupEventCriteria(String [,] groups)
        {
            for(int i = 0; i < groups.GetLength(0); i++)
            {
                for(int j = 0; j < groups.GetLength(1); j++)
                {
                    keyGroup[groups[i,j]] = i;
                }
            }
        }

        public static void ResetEventCount(String key, int v = 0)
        {
            PreferencesUtil.SetInteger(STATUS_PREFIX + key, v);
        }

        public static void EventOccured(String key, int dv = 1)
        {
            int v = PreferencesUtil.GetInteger(STATUS_PREFIX + key);
            PreferencesUtil.SetInteger(STATUS_PREFIX + key, v + dv);

            Workflow.AskForRating();
        }

        public static int GetEventCount(String key)
        {
            return PreferencesUtil.GetInteger(STATUS_PREFIX + key);
        }

        public static int GetEventCriteria(String key)
        {
            var cKey = CRITERIA_PREFIX + key;

            if (PreferencesUtil.HasKey(cKey))
            {
                return PreferencesUtil.GetInteger(cKey);
            }

            if (defaultCriteria.ContainsKey(cKey))
            {
                return defaultCriteria[cKey];
            }

            return 0;
        }

        public static void PopUpEnjoyment()
        {
            PreferencesUtil.SetBoolean(Criteria.REMIND_ME_LATER_CLICK, false); //reset the remind me
            PreferencesUtil.SetBoolean(Criteria.ALREADY_ASK_FOR_RATING, true);
        }

        public static void ClickReminderMe()
        {
            PreferencesUtil.SetBoolean(REMIND_ME_LATER_CLICK, true);

            PreferencesUtil.SetString(REMIND_ME_LATER_CLICK_TIME, DateTime.Now.ToString());
        }

        public static void SetFirstTimeLaunchTimestr(DateTime tm)
        {
            string firstLaunchTimeStr = tm.ToString();
            PreferencesUtil.SetString(FIRST_LAUNCH_TIME, firstLaunchTimeStr);
        }
        public static string GetFirstTimeLaunchTimestr()
        {
            return PreferencesUtil.GetString(FIRST_LAUNCH_TIME);
        }

        public static void InitFirstTimeLaunchTimestr()
        {
            if (!PreferencesUtil.HasKey(FIRST_LAUNCH_TIME))
            {
                SetFirstTimeLaunchTimestr(DateTime.Now);
            }
        }

        public static bool EnoughTimeAfterFirstLaunch()
        {
            //check how long after first launch
            DateTime now = DateTime.Now;
            string firstLaunchTimeStr = PreferencesUtil.GetString(FIRST_LAUNCH_TIME);
            DateTime firstLaunchTime = new DateTime();

            try
            {
                firstLaunchTime = Convert.ToDateTime(firstLaunchTimeStr);
            }
            catch (Exception e)
            {
                SetFirstTimeLaunchTimestr(DateTime.Now);
                return false;
            }

            if ((now - firstLaunchTime).TotalDays < GetEventCriteria(KEY_MIN_DAY_AFTER_FIRST_LAUNCH))
            {
                return false;
            }

            return true;
        }

        public static bool AlreadyPopup()
        {
            if (PreferencesUtil.HasKey(ALREADY_ASK_FOR_RATING) &&
                PreferencesUtil.GetBoolean(ALREADY_ASK_FOR_RATING) == true)
            {
                return true;
            }
            return false;
        }

        public static bool RemindMeLaterClicked()
        {
            if (PreferencesUtil.HasKey(REMIND_ME_LATER_CLICK))
            {
                return PreferencesUtil.GetBoolean(REMIND_ME_LATER_CLICK);
            }
            return false;
        }

        public static bool SatisfyCriteria()
        {
            //We only pop up the rating when user is online
            if (!AppServices.IsConnectedToInternet())
            {
                return false;
            }

            //Check whether it is enough time after first launch
            if (!EnoughTimeAfterFirstLaunch())
            {
                return false;
            }

            if (AlreadyPopup())
            {
                if (!RemindMeLaterClicked())
                {
                    //if not in reminder me status
                    return false;
                }

                //if and only if the customer clicked remind_me_later, we can pop up
                DateTime now = DateTime.Now;
                string remindMeLaterTimeStr = PreferencesUtil.GetString(REMIND_ME_LATER_CLICK_TIME);
                DateTime remindMeLaterTime = new DateTime();

                try
                {
                    remindMeLaterTime = Convert.ToDateTime(remindMeLaterTimeStr);
                }
                catch (Exception e)
                {
                    remindMeLaterTimeStr = DateTime.Now.ToString();
                    remindMeLaterTime = DateTime.Now;

                    PreferencesUtil.SetString(REMIND_ME_LATER_CLICK_TIME, remindMeLaterTimeStr);
                }

                //if it is less than 5 day after user click reminder me later, we cannot pop up
                if ((now - remindMeLaterTime).TotalDays < GetEventCriteria(KEY_TIME_GAP_BETWEEN_REMINDER_ME))
                {
                    return false;
                }

                //continue to see whether it satisfy other conditions
            }

            Dictionary<int, bool> groupSatisfy = new Dictionary<int, bool>();
            foreach(var item in keyGroup)
            {
                if (!groupSatisfy.ContainsKey(item.Value))
                {
                    groupSatisfy.Add(item.Value, false);
                }
            }

            //iterate all other criteria
            foreach (var key in keys)
            {

                if (key == CRITERIA_PREFIX + KEY_TIME_GAP_BETWEEN_REMINDER_ME)
                {
                    //already iterated
                    continue;
                }

                if (GetEventCount(key) < GetEventCriteria(key))
                {
                    if (!keyGroup.ContainsKey(key))
                    {
                        return false;
                    }
                }else
                {
                    if (keyGroup.ContainsKey(key))
                    {
                        groupSatisfy[keyGroup[key]] = true;
                    }
                }
            }

            //check whether all criteria group has satisfy the condition
            foreach(var item in groupSatisfy)
            {
                if (!item.Value)
                {
                    return false;
                }
            }


            return true;
        }

        public static void ResetCriteria()
        {
            PreferencesUtil.SetBoolean(Criteria.REMIND_ME_LATER_CLICK, false); //reset the remind me
            PreferencesUtil.SetBoolean(Criteria.ALREADY_ASK_FOR_RATING, false);

            SetFirstTimeLaunchTimestr(DateTime.Now);

            foreach (var key in keys)
            {
                ResetEventCount(key, 0);
            }
        }

        public static void ShowCriteria()
        {
            foreach (var key in keys)
            {
                Debug.Write(key + ": ");
                Debug.Write(GetEventCount(key) + "/");
                Debug.WriteLine(GetEventCriteria(key));
            }
        }

    }
}
