import { describe, expect, it, vi } from "vitest";

const renderMock = vi.fn();
const createRootMock = vi.fn(() => ({ render: renderMock }));
const reportWebVitalsMock = vi.fn();

vi.mock("react-dom/client", () => ({
  default: {
    createRoot: createRootMock,
  },
}));

vi.mock("./App", () => ({
  default: () => <div>Mock App</div>,
}));

vi.mock("./reportWebVitals", () => ({
  default: reportWebVitalsMock,
}));

describe("main", () => {
  it("creates a root and renders the app", async () => {
    document.body.innerHTML = '<div id="root"></div>';
    await import("./main");

    expect(createRootMock).toHaveBeenCalledWith(
      document.getElementById("root"),
    );
    expect(renderMock).toHaveBeenCalledTimes(1);
    expect(reportWebVitalsMock).toHaveBeenCalledTimes(1);
  });
});
