let autoScrollAndClean = async () => {
  const sleep = ms => new Promise(r => setTimeout(r, ms));

  let removedCount = 0;

  while (true) {
    const video = document.querySelector('ytd-playlist-video-renderer');
    if (!video) {
      console.log(`✅ All videos removed. Total removed: ${removedCount}`);
      break;
    }

    video.scrollIntoView({ behavior: 'smooth', block: 'center' });
    await sleep(500);

    const titleEl = video.querySelector('#video-title');
    const title = titleEl ? titleEl.textContent.trim() : 'Unknown Title';

    const menuBtn = video.querySelector('#button[aria-label][id="button"]');
    if (!menuBtn) {
      console.log(`⚠️ Skipping video (no menu): ${title}`);
      await sleep(10);
      continue;
    }

    menuBtn.click();
    await sleep(10);

    const menuItems = Array.from(document.querySelectorAll('ytd-menu-service-item-renderer'));
    const removeItem = menuItems.find(el => el.textContent.includes('Watch later'));
    if (removeItem) {
      removeItem.click();
      removedCount++;
      console.log(`🗑️ Removed: ${title}`);
    } else {
      console.log(`⚠️ Could not find 'Remove from Watch later' for: ${title}`);
    }

    await sleep(10); // wait for the list to update before next run
  }
};

autoScrollAndClean();



