function ErrorMessage( {title, message} ) {
    return (
        <div className="page-state">
            <div className="page-state-card error-state">

                <div className="error-icon">
                    !
                </div>

                <h2>{title}</h2>

                <p>{message}</p>

            </div>
        </div>
    );
}

export default ErrorMessage;