import { useState } from "react";

function ImageWithFallback({ src, alt, className = "" }) {
  const [imageError, setImageError] = useState(false);

  const hasImage = src && !imageError;
      const isAiGenerated = src?.startsWith("https://res.cloudinary.com");

  if (!hasImage) {
    return (
      <div className={`image-placeholder ${className}`}>
        <span>IV NEWS</span>
      </div>
    );
  }

  return (
    <div className="image-with-fallback">
      <img
        src={src}
        alt={alt}
        className={className}
        onError={() => setImageError(true)}
      />

      {isAiGenerated && (
        <span className="ai-generated-badge">
          <i className="bi bi-stars"></i>
          IA
        </span>
      )}
    </div>
  );
}

export default ImageWithFallback;
