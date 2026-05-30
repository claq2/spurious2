import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import Shell from "./Shell";

const navigateMock = vi.fn();

vi.mock("../components/NavBar", () => ({
  default: () => <div>NavBar</div>,
}));

vi.mock("react-router-dom", async () => {
  const actual =
    await vi.importActual<typeof import("react-router-dom")>(
      "react-router-dom",
    );
  return {
    ...actual,
    Outlet: () => <div>Outlet Content</div>,
    useLoaderData: () => [
      { shortName: "Beer", title: "Beer", address: "" },
      { shortName: "Wine", title: "Wine", address: "" },
    ],
    useParams: () => ({}),
    useLocation: () => ({ pathname: "/" }),
    useNavigate: () => navigateMock,
  };
});

describe("Shell", () => {
  it("renders shell content and redirects to the first density when missing id", () => {
    render(<Shell />);

    expect(
      screen.getByRole("heading", {
        name: /alcohol density per census subdivision/i,
      }),
    ).toBeInTheDocument();
    expect(screen.getByText("NavBar")).toBeInTheDocument();
    expect(screen.getByText("Outlet Content")).toBeInTheDocument();
    expect(navigateMock).toHaveBeenCalledWith("/beer", { replace: true });
  });
});
