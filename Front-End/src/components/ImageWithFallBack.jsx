import { useState } from "react";

function ImageWithFallback({ src, alt, className = "" }) {
  const [imageError, setImageError] = useState(false);

  const hasImage = src && !imageError;

  if (!hasImage) {
    return (
      <div className={`image-placeholder ${className}`}>
        <span>IV NEWS</span>
      </div>
    );
  }

  return (
    <img
      src={src}
      alt={alt}
      className={className}
      onError={() => setImageError(true)}
    />
  );
}

export default ImageWithFallback;
