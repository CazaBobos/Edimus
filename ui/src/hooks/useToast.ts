import { notifications } from "@mantine/notifications";

export const useToast = () => {
  const showSuccess = (message: string) => notifications.show({ message, color: "green" });
  const showError = (message: string) => notifications.show({ message, color: "red" });
  const showInfo = (message: string) => notifications.show({ message, color: "blue" });
  const clearToast = () => notifications.clean();

  return { showSuccess, showError, showInfo, clearToast };
};
