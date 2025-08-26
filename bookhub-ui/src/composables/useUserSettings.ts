import { reactive } from 'vue';

interface UserSettings {
  name: string;
  email: string;
  notifications: boolean;
  avatar?: string;
}

const defaultSettings: UserSettings = {
  name: '',
  email: '',
  notifications: true,
  avatar: ''
};

// reactive shared state
const settings = reactive<UserSettings>(
  JSON.parse(localStorage.getItem('userSettings') || '{}') || { ...defaultSettings }
);

function saveSettings() {
  localStorage.setItem('userSettings', JSON.stringify(settings));
}

function resetSettings() {
  Object.assign(settings, defaultSettings);
  localStorage.removeItem('userSettings');
}

export function useUserSettings() {
  return { settings, saveSettings, resetSettings };
}
