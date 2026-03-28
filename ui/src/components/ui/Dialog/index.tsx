"use client";

import { Modal } from "@mantine/core";
import { ReactNode } from "react";

type DialogProps = {
  open: boolean;
  onClose: () => void;
  children: ReactNode;
};

export const Dialog = ({ open, onClose, children }: DialogProps) => (
  <Modal
    opened={open}
    onClose={onClose}
    withCloseButton={false}
    centered
    size="auto"
    padding={0}
    styles={{
      overlay: { backdropFilter: "blur(4px)" },
    }}
  >
    {children}
  </Modal>
);
