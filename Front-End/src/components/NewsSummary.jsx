import React from "react";

function NewsSummary({ summary }) {
    return (
        <div className="news-summary">
            <p>{summary?.resumoExecutivo ?? "Resumo indisponível"}</p>
        </div>
    )
}

export default NewsSummary;