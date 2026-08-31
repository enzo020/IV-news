function NewsCard({ title, description }) {
    
    
    return (
        <article className="news-card">
            <h2 className="news-card-title">
                {title}
            </h2>

            <div className="news-card-image">
                <img alt="imagem da notícia"></img>
            </div>

            <p className="news-card-description">
                {description}
            </p>
        </article>
    );
}

export default NewsCard;