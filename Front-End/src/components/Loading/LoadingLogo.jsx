function LoadingLogo() {
    return (
        <svg
            className="loading-logo-svg"
            viewBox="-30 -43 170 170"
            xmlns="http://www.w3.org/2000/svg"
        >
            
            <g className="loading-lupa">

                <path
                    d="M 30 67 L 15 82"
                    fill="none"
                    stroke="#6F31C8"
                    strokeWidth="7"
                    strokeLinecap="round"
                />
                <path
                    d="M 30 67 L 15 82"
                    fill="none"
                    stroke="#9747FF"
                    strokeWidth="4.5"
                    strokeLinecap="round"
                />

                <path
                    d="M 16 81 L 4 93"
                    fill="none"
                    stroke="#6F31C8"
                    strokeWidth="13"
                    strokeLinecap="round"
                />
                <path
                    d="M 16 81 L 4 93"
                    fill="none"
                    stroke="#9747FF"
                    strokeWidth="10"
                    strokeLinecap="round"
                />
                <path
                    d="M 10 83 L 14 87 M 6 87 L 10 91"
                    fill="none"
                    stroke="#6F31C8"
                    strokeWidth="1.5"
                    strokeLinecap="round"
                />

                <circle
                    cx="55"
                    cy="42"
                    r="35"
                    fill="none"
                    stroke="#9747FF"
                    strokeWidth="5"
                />

            </g>

            <g className="loading-eye">
    
                <path
                    d="M 30 42 Q 55 22 80 42 Q 55 62 30 42 Z"
                    fill="#9747FF"
                />

                <circle
                    cx="55"
                    cy="42"
                    r="9"
                    fill="none"
                    stroke="white"
                    strokeWidth="3"
                />

                <circle
                    className="loading-pupil"
                    cx="55"
                    cy="42"
                    r="4"
                    fill="white"
                />

                <circle
                    cx="52"
                    cy="38"
                    r="2.5"
                    fill="white"
                />
            </g>

            <path
                className="loading-eyebrow"
                d="M 45 20 Q 55 14 65 20"
                fill="none"
                stroke="#9747FF"
                strokeWidth="4"
                strokeLinecap="round"
            />

        </svg>
    );
}

export default LoadingLogo;