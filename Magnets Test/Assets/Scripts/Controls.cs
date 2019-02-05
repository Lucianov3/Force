public class Controls
{
    public static bool TwoPlayerMode = false;
    public static bool PlayerOneAltControls = false;
    public static bool PlayerTwoAltControls = false;

    public static string RedPlayerMovementX
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Left Joystick X 1";
            }
            else
            {
                if (!PlayerOneAltControls)
                {
                    return "Left Joystick X 1";
                }
                else
                {
                    return "Right Joystick X 1";
                }
            }
        }
        private set
        {
        }
    }

    public static string RedPlayerMovementY
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Left Joystick Y 1";
            }
            else
            {
                if (!PlayerOneAltControls)
                {
                    return "Left Joystick Y 1";
                }
                else
                {
                    return "Right Joystick Y 1";
                }
            }
        }
        private set
        {
        }
    }

    public static string BluePlayerMovementX
    {
        get
        {
            if (!TwoPlayerMode)
            {
                if (!PlayerOneAltControls)
                {
                    return "Right Joystick X 1";
                }
                else
                {
                    return "Left Joystick X 1";
                }
            }
            else
            {
                if (!PlayerTwoAltControls)
                {
                    return "Left Joystick X 2";
                }
                else
                {
                    return "Right Joystick X 2";
                }
            }
        }
        private set
        {
        }
    }

    public static string BluePlayerMovementY
    {
        get
        {
            if (!TwoPlayerMode)
            {
                if (!PlayerOneAltControls)
                {
                    return "Left Joystick Y 1";
                }
                else
                {
                    return "Right Joystick Y 1";
                }
            }
            else
            {
                if (!PlayerTwoAltControls)
                {
                    return "Left Joystick Y 2";
                }
                else
                {
                    return "Right Joystick Y 2";
                }
            }
        }
        private set
        {
        }
    }

    public static string RedPlayerInteract
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Y Button 1";
            }
            else
            {
                if (!PlayerOneAltControls)
                {
                    return "A Button 1";
                }
                else
                {
                    return "Y Button 1";
                }
            }
        }
        private set
        {
        }
    }

    public static string BluePlayerInteract
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "A Button 1";
            }
            else
            {
                if (!PlayerTwoAltControls)
                {
                    return "Y Button 2";
                }
                else
                {
                    return "A Button 2";
                }
            }
        }
        private set
        {
        }
    }

    public static string RedPlayerHover
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Right Trigger 1";
            }
            else
            {
                if (!PlayerTwoAltControls)
                {
                    return "Left Trigger 2";
                }
                else
                {
                    return "Right Trigger 2";
                }
            }
        }
        private set
        {
        }
    }

    public static string BluePlayerHover
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Left Trigger 1";
            }
            else
            {
                if (!PlayerOneAltControls)
                {
                    return "Left Trigger 1";
                }
                else
                {
                    return "Right Trigger 1";
                }
            }
        }
        private set
        {
        }
    }

    public static string RedPlayerDuck
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Right Bumper 1";
            }
            else
            {
                if (!PlayerTwoAltControls)
                {
                    return "Left Bumper 2";
                }
                else
                {
                    return "Right Bumper 2";
                }
            }
        }
        private set
        {
        }
    }

    public static string BluePlayerDuck
    {
        get
        {
            if (!TwoPlayerMode)
            {
                return "Left Bumper 1";
            }
            else
            {
                if (!PlayerOneAltControls)
                {
                    return "Left Bumper 1";
                }
                else
                {
                    return "Right Bumper 1";
                }
            }
        }
        private set
        {
        }
    }
}