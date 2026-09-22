function LoadingLogo() {
    return (
        <svg
            className="loading-logo-svg"
            viewBox="-25 -25 150 150"
            xmlns="http://www.w3.org/2000/svg"
        >
            
            <g className="loading-lupa">

                <circle
                    cx="55"
                    cy="42"
                    r="35"
                    fill="none"
                    stroke="#9747FF"
                    strokeWidth="5"
                />

                <line
                    x1="30"
                    y1="67"
                    x2="10"
                    y2="88"
                    stroke="#9747FF"
                    strokeWidth="9"
                    strokeLinecap="round"
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